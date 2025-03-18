using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.CircleRequests;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Mapping.DTOs.CircleRequests;
using Mapping.Mappers;

namespace Cirkla_API.Services.CircleRequests;

public class CircleRequestService : ICircleRequestService
{
    private readonly ICircleRequestRepository _circleRequestRepository;
    private readonly ICircleRepository _circleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CircleRequestService> _logger;

    public CircleRequestService(ICircleRequestRepository circleRequestRepository, ICircleRepository circleRepository, IUserRepository userRepository, ILogger<CircleRequestService> logger)
    {
        _circleRequestRepository = circleRequestRepository;
        _circleRepository = circleRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    #region Getting

    public async Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForCircle(int circleId)
    {
        try
        {
            var requests = await _circleRequestRepository.GetAllByCircleId(circleId);
            return ServiceResult<IEnumerable<CircleRequest>>.Success(requests);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting all membership and admin requests for circle with {Id}", circleId);
            return ServiceResult<IEnumerable<CircleRequest>>.Fail("Error getting all requests for circle", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for circle with {Id}", circleId);
            return ServiceResult<IEnumerable<CircleRequest>>.Fail("Error getting all requests for circle", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForUser(string userId)
    {
        try
        {
            var requests = await _circleRequestRepository.GetAllByPendingMemberId(userId);
            return ServiceResult<IEnumerable<CircleRequest>>.Success(requests);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting all membership and admin requests for member with {Id}", userId);
            return ServiceResult<IEnumerable<CircleRequest>>.Fail("Error getting all requests for this member", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for member with {Id}", userId);
            return ServiceResult<IEnumerable<CircleRequest>>.Fail("Error getting all requests for this member", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> GetRequestById(int id)
    {
        try
        {
            var request = await _circleRequestRepository.GetById(id);
            if (request == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }

            return ServiceResult<CircleRequest>.Success(request);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error getting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error getting request", ErrorType.InternalError);
        }
    }

    #endregion


    #region Creating
    public async Task<ServiceResult<CircleRequest>> UserRequestsToJoin(CircleRequestCreateDTO circleRequestDTO)
    {
        // TODO: Check if user is already a member or admin
        if (circleRequestDTO.RequestType is not CircleJoinRequestType.UserRequestToJoin ||
            circleRequestDTO.FromUserId != circleRequestDTO.PendingMemberId)
        {
            _logger.LogError("Invalid request for user with ID {UserId} to join circle with ID {CircleId}", circleRequestDTO.FromUserId, circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            // TODO: Use mapping
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to circle admins
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> UserRequestsToBecomeAdmin(CircleRequestCreateDTO circleRequestDTO)
    {
        if (circleRequestDTO.RequestType is not CircleJoinRequestType.UserRequestToBecomeAdmin ||
            circleRequestDTO.FromUserId != circleRequestDTO.PendingMemberId)
        {
            _logger.LogError("Invalid request type for admin request to join circle with {Id}",
                circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to circle admins
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> MemberInvitesUser(CircleRequestCreateDTO circleRequestDTO)
    {
        if (circleRequestDTO.RequestType is not CircleJoinRequestType.MemberInviteToUser ||
            circleRequestDTO.FromUserId == circleRequestDTO.PendingMemberId)
        {
            _logger.LogError("Invalid invite to user with {UserId} for circle with {CircleId}", circleRequestDTO.PendingMemberId, circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }
        try
        {
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to invited user
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> MembershipInviteFromAdmin(CircleRequestCreateDTO circleRequestDTO)
    {
        if (circleRequestDTO.RequestType is not CircleJoinRequestType.MembershipInviteFromAdmin)
            // TODO: Check if user is an admin of the circle
            //!circleRequestDTO.Circle.Administrators.Any(admin => admin.Id == circleRequestDTO.FromUser.Id
        {
            _logger.LogError("Invalid membership invite from admin with {UserId} for circle with {CircleId}", circleRequestDTO.PendingMemberId, circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> AdminInviteFromAdmin(CircleRequestCreateDTO circleRequestDTO)
    {
        if (circleRequestDTO.RequestType is not CircleJoinRequestType.AdminInviteFromAdmin)
            // TODO: Check if user is an admin of the circle
            //!circleRequestDTO.Circle.Administrators.Any(admin => admin.Id == circleRequestDTO.FromUser.Id
        {
            _logger.LogError("Invalid admin invite from admin with {UserId} for circle with {CircleId}", circleRequestDTO.PendingMemberId, circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequestDTO.CircleId);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }

    #endregion


    #region Updating
    public async Task<ServiceResult<CircleRequest>> RejectRequest(int id, CircleRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Pending ||
            circleRequestDTO.UpdatedByUserId == circleRequestDTO.FromUserId)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleRequestRepository.Update(requestToDb);
            await _circleRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error rejecting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error rejecting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error rejecting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error rejecting request", ErrorType.InternalError);
        }
    }

    // TODO: Refactor to accept UserId and CircleId to make the whole operation in the backend?
    public async Task<ServiceResult<CircleRequest>> RevokeRequest(int id, CircleRequestUpdateDTO circleRequestDTO)
    {
        // TODO: Check:
        // if user is the one who created the request,
        // if request hasn't been answered already
        // if request hasn't expired
        // Put all these checks in separate methods for each type of status change

        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Revoked)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleRequestRepository.Update(requestToDb);
            await _circleRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error revoking request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error revoking request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error revoking request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error revoking request", ErrorType.InternalError);
        }
    }

    // TODO: Expired, unanswered requests will be handled by a background service

    public async Task<ServiceResult<CircleRequest>> AdminAcceptsRequest(int id, CircleRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
            circleRequestDTO.UpdatedByUserId == circleRequestDTO.FromUserId)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            await _circleRepository.UpdateMembers(requestToDb.Circle);
            await _circleRepository.UpdateAdministrators(requestToDb.Circle);
            var updatedRequest = await _circleRequestRepository.Update(requestToDb);
            await _circleRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error accepting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
    }

    public async Task<ServiceResult<CircleRequest>> UserAcceptsInvite(int id, CircleRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
            circleRequestDTO.PendingMemberId != circleRequestDTO.FromUserId)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var newMember = await _userRepository.Get(circleRequestDTO.PendingMemberId);
            if (circleRequestDTO.RequestType == CircleJoinRequestType.MemberInviteToUser ||
                circleRequestDTO.RequestType == CircleJoinRequestType.MembershipInviteFromAdmin)
            {
                circle.Members.Add(newMember);
                await _circleRepository.UpdateMembers(circle);
            }
            else if (circleRequestDTO.RequestType == CircleJoinRequestType.AdminInviteFromAdmin)
            {
                circle.Administrators.Add(newMember);
                await _circleRepository.UpdateAdministrators(circle);
            }
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleRequestRepository.Update(requestToDb);
            await _circleRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }

            return ServiceResult<CircleRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error accepting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
    }

    #endregion
}
