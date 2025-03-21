using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Services.CircleMembership;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.CircleJoinRequests;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Mapping.DTOs.CircleJoinRequests;
using Mapping.Mappers;

namespace Cirkla_API.Services.CircleMembership;

public partial class CircleMembershipService : ICircleMembershipService
{
    private readonly ICircleJoinRequestRepository _circleJoinRequestRepository;
    private readonly ICircleRepository _circleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CircleMembershipService> _logger;

    public CircleMembershipService(ICircleJoinRequestRepository circleJoinRequestRepository,
        ICircleRepository circleRepository, IUserRepository userRepository, ILogger<CircleMembershipService> logger)
    {
        _circleJoinRequestRepository = circleJoinRequestRepository;
        _circleRepository = circleRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    #region Getting

    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForCircle(int circleId)
    {
        try
        {
            var requests = await _circleJoinRequestRepository.GetAllByCircleId(circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting all membership and admin requests for circle with {Id}", circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for circle",
                ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for circle with {Id}",
                circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for circle",
                ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForUser(string userId)
    {
        try
        {
            var requests = await _circleJoinRequestRepository.GetAllByTargetUserId(userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting all membership and admin requests for member with {Id}", userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for this member",
                ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for member with {Id}",
                userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for this member",
                ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleJoinRequest>> GetRequestById(int id)
    {
        try
        {
            var request = await _circleJoinRequestRepository.GetById(id);
            if (request == null)
            {
                return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
            }

            return ServiceResult<CircleJoinRequest>.Success(request);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error getting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error getting request", ErrorType.InternalError);
        }
    }

    #endregion


    #region Creating

    public async Task<ServiceResult<CircleJoinRequest>> SendJoinRequest(CircleJoinRequestCreateDTO circleRequestDTO)
    {
        // If null, expired or NOT pending - return fail
        if (!await PassesFirstCheck(circleRequestDTO))
        {
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        // Fetch necessary data from the database and map to the entity for validation
        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
        var targetUser = await _userRepository.Get(circleRequestDTO.TargetUserId);
        var fromUser = await _userRepository.Get(circleRequestDTO.FromUserId);
        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle, targetUser, fromUser);

        if (await AlreadyInvited(requestToDb))
        {
            _logger.LogWarning("User with ID {UserId} already invited to circle with ID {CircleId}",
                requestToDb.TargetUserId, requestToDb.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("User already invited", ErrorType.ValidationError);
        }

        // What is the request for? Member, admin? => fork paths (call different methods)
        if (requestToDb.Type == CircleJoinRequestType.JoinAsMember)
        {
            // Call membership method
            return await CreateMembershipRequest(requestToDb);
        }
        else if (requestToDb.Type == CircleJoinRequestType.JoinAsAdmin)
        {
            return await CreateAdminRequest(requestToDb);
        }

        return ServiceResult<CircleJoinRequest>.Fail("Invalid request type", ErrorType.ValidationError);
    }

    public async Task<ServiceResult<CircleJoinRequest>> CreateMembershipRequest(CircleJoinRequest request)
    {
        if (!await CanJoinAsMember(request))
        {
            _logger.LogError("Invalid request to add member with ID {UserId} to circle with ID {CircleId}",
                request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        if (!await CanInviteMembers(request))
        {
            _logger.LogError(
                "User with ID {UpdatingUser} not authorized to invite member with ID {UserId} to circle with ID {CircleId}.",
                request.FromUserId, request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var createdRequest = await _circleJoinRequestRepository.Create(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Created(createdRequest);
    }

    public async Task<ServiceResult<CircleJoinRequest>> CreateAdminRequest(CircleJoinRequest request)
    {
        if (!await CanInviteAdmin(request))
        {
            _logger.LogError(
                "User with ID {UpdatingUser} not authorized to add member with ID {UserId} to circle with ID {CircleId}.",
                request.FromUserId, request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var createdRequest = await _circleJoinRequestRepository.Create(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Created(createdRequest);
    }

    #endregion


    #region Updating

    public async Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id,
        CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanRevoke(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            _logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        existingRequest.Status = CircleRequestStatus.Revoked;
        existingRequest.UpdatedAt = DateTime.Now;
        var updatedRequest = await _circleJoinRequestRepository.Update(existingRequest);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    }

    public async Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id,
        CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanReject(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            _logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        existingRequest.Status = CircleRequestStatus.Rejected;
        existingRequest.UpdatedAt = DateTime.Now;
        var updatedRequest = await _circleJoinRequestRepository.Update(existingRequest);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    }

    // TODO: Expired, unanswered requests will be handled by a background service


    public async Task<ServiceResult<CircleJoinRequest>> AcceptRequest(int id,
        CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanAccept(existingRequest) ||
            existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            _logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        // TODO: These methods only add users.
        await UpdateCircleMembers(existingRequest);
        await UpdateCircleAdmins(existingRequest);
        existingRequest.Status = CircleRequestStatus.Accepted;
        existingRequest.UpdatedAt = DateTime.Now;
        var updatedRequest = await _circleJoinRequestRepository.Update(existingRequest);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    }

    #endregion

    #region Admin actions

    public async Task<ServiceResult<CircleJoinRequest>> MakeAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
    {
        if (adminRequestDTO.Type != CircleJoinRequestType.JoinAsAdmin)
        {
            _logger.LogError("Invalid request type for making admin");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        var circle = await _circleRepository.GetById(adminRequestDTO.CircleId);
        var targetUser = await _userRepository.Get(adminRequestDTO.TargetUserId);
        var fromUser = await _userRepository.Get(adminRequestDTO.FromUserId);
        var updatedByUser = await _userRepository.Get(adminRequestDTO.UpdatedByUserId);

        var requestToDb = await Mapper.MapToCircleRequest(adminRequestDTO, circle, targetUser, fromUser);
        requestToDb.UpdatedByUser = updatedByUser;
        requestToDb.UpdatedAt = DateTime.Now;
        if (await IsFromAdmin(requestToDb) && await CanInviteAdmin(requestToDb))
        {
            circle.Administrators.Add(targetUser);
            await _circleRepository.UpdateAdministrators(circle);
            await _circleJoinRequestRepository.Create(requestToDb);
            await _circleJoinRequestRepository.SaveChanges();
            return ServiceResult<CircleJoinRequest>.Created(requestToDb);
        }
        return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
    }


    // TODO: Quick and dirty solution, takes a joinrequest DTO but doesn't actually use it against db. Refactor with specific DTO.
    public async Task<ServiceResult<CircleJoinRequest>> RemoveAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
    {
        if (adminRequestDTO.Type != CircleJoinRequestType.JoinAsAdmin)
        {
            _logger.LogError("Invalid request type for making admin");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        var circle = await _circleRepository.GetById(adminRequestDTO.CircleId);
        var targetUser = await _userRepository.Get(adminRequestDTO.TargetUserId);
        var fromUser = await _userRepository.Get(adminRequestDTO.FromUserId);
        var updatedByUser = await _userRepository.Get(adminRequestDTO.UpdatedByUserId);

        var requestToDb = await Mapper.MapToCircleRequest(adminRequestDTO, circle, targetUser, fromUser);
        requestToDb.UpdatedByUser = updatedByUser;
        requestToDb.UpdatedAt = DateTime.Now;
        if (await IsFromAdmin(requestToDb) && await CanInviteAdmin(requestToDb))
        {
            circle.Administrators.Remove(targetUser);
            await _circleRepository.UpdateAdministrators(circle);
            await _circleRepository.SaveChanges();
            return ServiceResult<CircleJoinRequest>.Created(requestToDb);
        }
        return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
    }

    #endregion
}
