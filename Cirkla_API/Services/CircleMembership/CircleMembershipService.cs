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

namespace Cirkla_API.Services.CircleRequests;

public class CircleMembershipService : ICircleMembershipService
{
    private readonly ICircleJoinRequestRepository _circleJoinRequestRepository;
    private readonly ICircleRepository _circleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CircleMembershipService> _logger;

    public CircleMembershipService(ICircleJoinRequestRepository circleJoinRequestRepository, ICircleRepository circleRepository, IUserRepository userRepository, ILogger<CircleMembershipService> logger)
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
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for circle", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for circle with {Id}", circleId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for circle", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForUser(string userId)
    {
        try
        {
            var requests = await _circleJoinRequestRepository.GetAllByTargetMemberId(userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Success(requests);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error getting all membership and admin requests for member with {Id}", userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for this member", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error getting all membership and admin requests for member with {Id}", userId);
            return ServiceResult<IEnumerable<CircleJoinRequest>>.Fail("Error getting all requests for this member", ErrorType.InternalError);
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

    public async Task<ServiceResult<CircleJoinRequest>> RequestToJoin(CircleJoinRequestCreateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null)
        {
            _logger.LogError("Invalid request");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO);
        var createdRequest = await _circleJoinRequestRepository.Create(requestToDb);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(createdRequest);
    }

    #endregion


    #region Updating
    public async Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Pending ||
            circleRequestDTO.UpdatedByUserId == circleRequestDTO.FromUserId)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
            await _circleJoinRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error rejecting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error rejecting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error rejecting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error rejecting request", ErrorType.InternalError);
        }
    }

    // TODO: Refactor to accept UserId and CircleId to make the whole operation in the backend?
    public async Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
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
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
            await _circleJoinRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error revoking request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error revoking request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error revoking request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error revoking request", ErrorType.InternalError);
        }
    }

    // TODO: Expired, unanswered requests will be handled by a background service

    public async Task<ServiceResult<CircleJoinRequest>> AdminAcceptsRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
            circleRequestDTO.UpdatedByUserId != circleRequestDTO.FromUserId)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var newMember = await _userRepository.Get(circleRequestDTO.TargetMemberId);
            if (true) // TODO: <- Remove this line, for debugging only
            {
                circle.Members.Add(newMember);
                await _circleRepository.UpdateMembers(circle);
            }
            else if (true)
            {
                circle.Administrators.Add(newMember);
                await _circleRepository.UpdateAdministrators(circle);
            }
            else
            {
                // TODO: This chould be checked earlier
                return ServiceResult<CircleJoinRequest>.Fail($"Request type: '{circleRequestDTO.Type}' not valid for this method", ErrorType.ValidationError);
            }
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            await _circleRepository.UpdateMembers(requestToDb.Circle);
            await _circleRepository.UpdateAdministrators(requestToDb.Circle);
            var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
            await _circleJoinRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error accepting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
    }

    public async Task<ServiceResult<CircleJoinRequest>> UserAcceptsInvite(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO is null ||
            circleRequestDTO.Id != id ||
            circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
            circleRequestDTO.TargetMemberId != circleRequestDTO.FromUserId) // TODO: <- Change this to ==
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
            var newMember = await _userRepository.Get(circleRequestDTO.TargetMemberId);
            if (true) // TODO: <- Remove this line, for debugging only
            {
                circle.Members.Add(newMember);
                await _circleRepository.UpdateMembers(circle);
            }
            else if (true)
            {
                circle.Administrators.Add(newMember);
                await _circleRepository.UpdateAdministrators(circle);
            }
            var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
            var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
            await _circleJoinRequestRepository.SaveChanges();
            if (updatedRequest == null)
            {
                return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
            }

            return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error accepting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
        }
    }

    #endregion


    #region Validation helpers
    // Early return:
    // Is the request expired?
    // Is the request still unanswered? (Could be easily inferred from the status, no need for validation method)

    // From which user role is the request coming from?
    // Is the request coming from the user herself?
    // Is the invitation coming from a member?
    // Is the invitation coming from an admin?

    // Is the request valid?
    // CanJoin => not already a member or admin
    // CanInvite => mut be a member or admin
    // CanInviteAdmin => must be an admin

    // Who answered/updated the request?
    // CanRevoke => only the user who created the request, while request is still pending
    // CanAccept => only the target user or member/admin, depending on request/invitation
    // CanReject => only the target user or member/admin, depending on request/invitation

    // Validation tree:
    // EARLY RETURN: Expired? Already answered? => return
    // TYPE OF REQUEST: Member or admin? => different paths
    // REQUEST VALIDATION: CanJoin, CanInvite, CanInviteAdmin => return if not valid
    // WHO ANSWERED: CanRevoke, CanAccept, CanReject => return if not valid



    private async Task<bool> IsRequestFromUser(CircleJoinRequestCreateDTO request)
    {
        if (request.FromUserId != request.TargetUserId)
        {
            return false;
        }
        return true;
    }

    //private async Task<bool> IsInvitationFromMember()
    //{
    //}

    //private async Task<bool> IsInvitationFromAdmin()
    //{
    //}

    //private async Task<bool> IsNotExpired()
    //{
    //}



    #endregion
}
