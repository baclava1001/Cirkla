using Azure.Core;
using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.CircleJoinRequests;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Cirkla.Shared.Mappers;
using FluentValidation;
using Cirkla.Shared.DTOs.CircleJoinRequests;
using Cirkla.Shared.Validators.CircleJoinRequests;

namespace Cirkla_API.Services.CircleMembership;

public partial class CircleMembershipService(
    ICircleJoinRequestRepository circleJoinRequestRepository,
    ICircleRepository circleRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    CircleJoinRequestCreateValidator circleJoinRequestCreateValidator,
    CircleJoinRequestUpdateValidator circleJoinRequestUpdateValidator,
    ILogger<CircleMembershipService> logger) : ICircleMembershipService
{
    #region Getting

    // TODO: Add a response DTO for these methods?
    public async Task<ServiceResult<CircleJoinRequest>> GetRequestById(int id)
    {
        var request = await circleJoinRequestRepository.GetById(id);
        if (request is null)
        {
            logger.LogInformation("Request with ID {Id} not found", id);
            return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
        }
        return ServiceResult<CircleJoinRequest>.Success(request);
    }


    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForCircle(int circleId)
    {
        var requests = await circleJoinRequestRepository.GetAllByCircleId(circleId);
        if (requests is null || !requests.Any())
        {
            logger.LogInformation("No requests found for circle with ID {CircleId}", circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("No requests found", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
    }


    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForUser(string userId)
    {
        var requests = await circleJoinRequestRepository.GetAllByTargetUserId(userId);
        if (requests is null || !requests.Any())
        {
            logger.LogInformation("No requests found for user with ID {UserId}", userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("No requests found", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
    }


    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForUserAndCircle(string targetUserId,
        int circleId)
    {
        var requests = await circleJoinRequestRepository.GetByTargetUserAndCircle(targetUserId, circleId);
        if (requests is null || !requests.Any())
        {
            logger.LogInformation("No requests found for user with ID {UserId} to join circle with ID {CircleId}", targetUserId, circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("No requests found", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
    }

    #endregion


    #region Creating

    public async Task<ServiceResult<int>> SendJoinRequest(CircleJoinRequestCreateDTO circleRequestDTO)
    {
        var validationResult = await circleJoinRequestCreateValidator.ValidateAsync(circleRequestDTO);
        if (!validationResult.IsValid)
        {
            return LogAndReturnValidationError<int>(validationResult.Errors);
        }

        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
        await HydrateRequestAsync(requestToDb);

        if (await AlreadyInvited(requestToDb))
        {
            logger.LogWarning("User with ID {UserId} already invited to or a member in circle with ID {CircleId}",
                requestToDb.TargetUserId, requestToDb.CircleId);
            return ServiceResult<int>.Fail("User already invited or a member", ErrorType.ValidationError);
        }

        return requestToDb.Type switch
        {
            CircleJoinRequestType.JoinAsMember => await CreateMembershipRequest(requestToDb),
            CircleJoinRequestType.JoinAsAdmin => await CreateAdminRequest(requestToDb),
            _ => ServiceResult<int>.Fail("Invalid request type", ErrorType.ValidationError)
        };
    }


    public async Task<ServiceResult<int>> CreateMembershipRequest(CircleJoinRequest request)
    {
        if (await CanInviteMembers(request) || await CanJoinAsMember(request))
        {
            return await CreateRequest(request);
        }
        logger.LogError("Invalid request to add member with ID {UserId} to circle with ID {CircleId}", request.TargetUserId, request.CircleId);
        return ServiceResult<int>.Fail("Invalid request", ErrorType.ValidationError);
    }

    // TODO: Method not used for now, needs refactoring so members can ask to become admins and reply to admin invitations
    public async Task<ServiceResult<int>> CreateAdminRequest(CircleJoinRequest request)
    {
        if (!await CanInviteAdmin(request))
        {
            logger.LogError("User with ID {UpdatingUser} not authorized to add member with ID {UserId} to circle with ID {CircleId}", request.FromUserId, request.TargetUserId, request.CircleId);
            return ServiceResult<int>.Fail("Invalid request", ErrorType.ValidationError);
        }
        return await CreateRequest(request);
    }

    #endregion


    #region Updating

    public async Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        var validationResult = await circleJoinRequestUpdateValidator.ValidateAsync(circleRequestDTO);
        if (!validationResult.IsValid)
        {
            return LogAndReturnValidationError<CircleJoinRequest>(validationResult.Errors);
        }

        var existingRequest = await circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await InitialValidation(existingRequest) ||
            !await CanRevoke(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        return await UpdateRequest(existingRequest, CircleRequestStatus.Revoked);
    }


    public async Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        var validationResult = await circleJoinRequestUpdateValidator.ValidateAsync(circleRequestDTO);
        if (!validationResult.IsValid)
        {
            return LogAndReturnValidationError<CircleJoinRequest>(validationResult.Errors);
        }

        var existingRequest = await circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await InitialValidation(existingRequest) ||
            !await CanReject(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        return await UpdateRequest(existingRequest, CircleRequestStatus.Rejected);
    }


    // TODO: Expired, unanswered requests will be handled by a background service


    public async Task<ServiceResult<CircleJoinRequest>> AcceptRequest(int id,
        CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        var validationResult = await circleJoinRequestUpdateValidator.ValidateAsync(circleRequestDTO);
        if (!validationResult.IsValid)
        {
            return LogAndReturnValidationError<CircleJoinRequest>(validationResult.Errors);
        }

        var existingRequest = await circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await InitialValidation(existingRequest) ||
            !await CanAccept(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        return await UpdateRequest(existingRequest, CircleRequestStatus.Accepted);
    }

    #endregion

    #region Admin actions

    public async Task<ServiceResult<int>> MakeAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
    {
        if (adminRequestDTO.Type != CircleJoinRequestType.JoinAsAdmin)
        {
            logger.LogError("Invalid request type for making admin");
            return ServiceResult<int>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        var requestToDb = await Mapper.MapToCircleRequest(adminRequestDTO);
        await HydrateRequestAsync(requestToDb);

        requestToDb.UpdatedAt = DateTime.Now;
        requestToDb.Status = CircleRequestStatus.Accepted;

        if (await IsFromAdmin(requestToDb) && await CanInviteAdmin(requestToDb))
        {
            requestToDb.Circle.Administrators.Add(requestToDb.TargetUser);
            await circleRepository.UpdateAdministrators(requestToDb.Circle);
            return await CreateRequest(requestToDb);
        }
        return ServiceResult<int>.Fail("Invalid request", ErrorType.ValidationError);
    }


    public async Task<ServiceResult<int>> RemoveAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
    {
        if (adminRequestDTO.Type != CircleJoinRequestType.JoinAsAdmin)
        {
            logger.LogError("Invalid request type for removing admin");
            return ServiceResult<int>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        var requestToDb = await Mapper.MapToCircleRequest(adminRequestDTO);
        await HydrateRequestAsync(requestToDb);
        requestToDb.UpdatedAt = DateTime.Now;

        if (requestToDb.Circle.Administrators.Count > 1 &&
            await IsFromAdmin(requestToDb) &&
            await CanInviteAdmin(requestToDb))
        {
            requestToDb.Circle.Administrators.Remove(requestToDb.TargetUser);
            await circleRepository.UpdateAdministrators(requestToDb.Circle);
            return await CreateRequest(requestToDb);
        }
        return ServiceResult<int>.Fail("Invalid request", ErrorType.ValidationError);
    }

    // TODO: Add a method for removing members

    #endregion
}
