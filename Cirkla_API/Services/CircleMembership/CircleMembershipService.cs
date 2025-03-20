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
            var requests = await _circleJoinRequestRepository.GetAllByTargetUserId(userId);
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

    public async Task<ServiceResult<CircleJoinRequest>> SendJoinRequest(CircleJoinRequestCreateDTO circleRequestDTO)
    {
        // If null, expired or NOT pending - return fail
        if (!await PassesFirstCheck(circleRequestDTO))
        {
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        // (Fetch necessary data from the database and map to the entity for validation)
        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
        var targetUser = await _userRepository.Get(circleRequestDTO.TargetUserId);
        var fromUser = await _userRepository.Get(circleRequestDTO.FromUserId);
        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle, targetUser, fromUser);

        if (await AlreadyInvited(requestToDb))
        {
            _logger.LogWarning("User with ID {UserId} already invited to circle with ID {circleId}", requestToDb.TargetUserId, requestToDb.CircleId);
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
            // Call admin method
            // return CreateAdminRequest();
        }
        return ServiceResult<CircleJoinRequest>.Fail("Invalid request type", ErrorType.ValidationError);
    }

    public async Task<ServiceResult<CircleJoinRequest>> CreateMembershipRequest(CircleJoinRequest request)
    {
        if (!await CanJoinAsMember(request) && !await CanInviteMembers(request))
        {
            _logger.LogError("Invalid request to add member with ID {UserId} to circle with ID {CircleId}", request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        var createdRequest = await _circleJoinRequestRepository.Create(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(createdRequest);
    }

    public async Task<ServiceResult<CircleJoinRequest>> CreateAdminRequest(CircleJoinRequest request)
    {
        if (!await CanInviteAdmin(request))
        {
            _logger.LogError("User with ID {UpdatingUser} not authorized to add member with ID {UserId} to circle with ID {CircleId}.", request.FromUserId, request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        var createdRequest = await _circleJoinRequestRepository.Create(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(createdRequest);
    }

    #endregion


    #region Updating

    // TODO: Refactor to accept UserId and CircleId to make the whole operation in the backend?
    public async Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        var request = await Mapper.MapToCircleRequest(circleRequestDTO, existingRequest);

        if (!await PassesFirstCheck(request) || !await CanRevoke(request) || request.CircleId != circleRequestDTO.CircleId)
        {
            _logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        request.Status = CircleRequestStatus.Revoked;
        request.UpdatedAt = DateTime.Now;
        var updatedRequest = await _circleJoinRequestRepository.Update(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    }

    //public async Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    //{
    //    if (circleRequestDTO is null ||
    //        circleRequestDTO.Id != id ||
    //        circleRequestDTO.Status is not CircleRequestStatus.Pending ||
    //        circleRequestDTO.UpdatedByUserId == circleRequestDTO.FromUserId)
    //    {
    //        _logger.LogError("Invalid request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
    //    }

    //    try
    //    {
    //        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
    //        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
    //        var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
    //        await _circleJoinRequestRepository.SaveChanges();
    //        if (updatedRequest == null)
    //        {
    //            return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
    //        }
    //        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    //    }
    //    catch (DbUpdateException ex)
    //    {
    //        _logger.LogError(ex, "Error rejecting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error rejecting request", ErrorType.InternalError);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Unexpected error rejecting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error rejecting request", ErrorType.InternalError);
    //    }
    //}

    // TODO: Expired, unanswered requests will be handled by a background service

    //public async Task<ServiceResult<CircleJoinRequest>> AdminAcceptsRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    //{
    //    if (circleRequestDTO is null ||
    //        circleRequestDTO.Id != id ||
    //        circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
    //        circleRequestDTO.UpdatedByUserId != circleRequestDTO.FromUserId)
    //    {
    //        _logger.LogError("Invalid request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
    //    }
    //    try
    //    {
    //        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
    //        var newMember = await _userRepository.Get(circleRequestDTO.TargetUserId);
    //        if (true) // TODO: <- Remove this line, for debugging only
    //        {
    //            circle.Members.Add(newMember);
    //            await _circleRepository.UpdateMembers(circle);
    //        }
    //        else if (true)
    //        {
    //            circle.Administrators.Add(newMember);
    //            await _circleRepository.UpdateAdministrators(circle);
    //        }
    //        else
    //        {
    //            // TODO: This chould be checked earlier
    //            return ServiceResult<CircleJoinRequest>.Fail($"Request type: '{circleRequestDTO.Type}' not valid for this method", ErrorType.ValidationError);
    //        }
    //        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
    //        await _circleRepository.UpdateMembers(requestToDb.Circle);
    //        await _circleRepository.UpdateAdministrators(requestToDb.Circle);
    //        var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
    //        await _circleJoinRequestRepository.SaveChanges();
    //        if (updatedRequest == null)
    //        {
    //            return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
    //        }
    //        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    //    }
    //    catch (DbUpdateException ex)
    //    {
    //        _logger.LogError(ex, "Error accepting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
    //    }
    //}

    //public async Task<ServiceResult<CircleJoinRequest>> UserAcceptsInvite(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    //{
    //    if (circleRequestDTO is null ||
    //        circleRequestDTO.Id != id ||
    //        circleRequestDTO.Status is not CircleRequestStatus.Accepted ||
    //        circleRequestDTO.TargetUserId != circleRequestDTO.FromUserId) // TODO: <- Change this to ==
    //    {
    //        _logger.LogError("Invalid request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
    //    }

    //    try
    //    {
    //        var circle = await _circleRepository.GetById(circleRequestDTO.CircleId);
    //        var newMember = await _userRepository.Get(circleRequestDTO.TargetUserId);
    //        if (true) // TODO: <- Remove this line, for debugging only
    //        {
    //            circle.Members.Add(newMember);
    //            await _circleRepository.UpdateMembers(circle);
    //        }
    //        else if (true)
    //        {
    //            circle.Administrators.Add(newMember);
    //            await _circleRepository.UpdateAdministrators(circle);
    //        }
    //        var requestToDb = await Mapper.MapToCircleRequest(circleRequestDTO, circle);
    //        var updatedRequest = await _circleJoinRequestRepository.Update(requestToDb);
    //        await _circleJoinRequestRepository.SaveChanges();
    //        if (updatedRequest == null)
    //        {
    //            return ServiceResult<CircleJoinRequest>.Fail("Request not found", ErrorType.NotFound);
    //        }

    //        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
    //    }
    //    catch (DbUpdateException ex)
    //    {
    //        _logger.LogError(ex, "Error accepting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Unexpected error accepting request with {Id}", id);
    //        return ServiceResult<CircleJoinRequest>.Fail("Error accepting request", ErrorType.InternalError);
    //    }
    //}

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
    // CanInvite => must be a member or admin
    // CanInviteAdmin => must be an admin

    // Who answered/updated the request?
    // CanRevoke => only the user who created the request, while request is still pending
    // CanAccept => only the target user or member/admin, depending on request/invitation
    // CanReject => only the target user or member/admin, depending on request/invitation

    // Validation tree:
    // CREATION:
    // EARLY RETURN: Expired? Already answered? => return
    // AlreadyInvited? => return
    // TYPE OF REQUEST: Member or admin? => different paths
    // REQUEST VALIDATION: CanJoin, CanInvite, CanInviteAdmin => return if not valid

    // UPDATING:
    // WHO ANSWERED: CanRevoke, CanAccept, CanReject => return if not valid



    // Early return
    private async Task<bool> IsExpired(DateTime? expiresAt)
    {
        return (expiresAt < DateTime.Now);
    }

    private async Task<bool> PassesFirstCheck(CircleJoinRequestCreateDTO circleJoinRequestDTO)
    {
        if (circleJoinRequestDTO is null || await IsExpired(circleJoinRequestDTO.ExpiresAt) || circleJoinRequestDTO.Status != CircleRequestStatus.Pending)
        {
            _logger.LogError("Invalid request is null, expired or already answered");
            return false;
        }
        return true;
    }

    private async Task<bool> PassesFirstCheck(CircleJoinRequest request)
    {
        if (request is null || await IsExpired(request.ExpiresAt) || request.Status != CircleRequestStatus.Pending)
        {
            _logger.LogError("Invalid request is null, expired or already answered");
            return false;
        }
        return true;
    }

    private async Task<bool> AlreadyInvited(CircleJoinRequest request)
    {
        var requests = await _circleJoinRequestRepository.GetAllByCircleId(request.CircleId);
        return requests.Any(r => r.TargetUserId == request.TargetUserId);
    }
    
    // CanJoin => not already a member (all admins are also members)
    private async Task<bool> CanJoinAsMember(CircleJoinRequest request)
    {
        return (!request.Circle.Members.Contains(request.TargetUser));
    }

    // CanInvite => must be a member or admin
    private async Task<bool> CanInviteMembers(CircleJoinRequest request)
    {
        return (request.Circle.Members.Contains(request.FromUser) || request.Circle.Administrators.Contains(request.FromUser));
    }

    // CanInviteAdmin => must be an admin
    private async Task<bool> CanInviteAdmin(CircleJoinRequest request)
    {
        return request.Circle.Administrators.Contains(request.FromUser);
    }


    // Check role of request sender before updating a request
    private async Task<bool> IsRequestFromUser(CircleJoinRequest request)
    {
        return (request.FromUserId == request.TargetUserId &&
                !request.Circle.Members.Contains(request.FromUser));
    }

    private async Task<bool> IsInvitationFromMember(CircleJoinRequest request)
    {
        return (request.Circle.Members.Contains(request.FromUser) || !request.Circle.Administrators.Contains(request.FromUser));
    }

    private async Task<bool> IsInvitationFromAdmin(CircleJoinRequest request)
    {
        return request.Circle.Administrators.Contains(request.FromUser);
    }

    // CanRevoke => only the user who created the request, while request is still pending
    private async Task<bool> CanRevoke(CircleJoinRequest request)
    {
        return (request.Status == CircleRequestStatus.Pending && request.UpdatedByUserId == request.FromUserId);
    }

    // CanAccept => only the target user or member/admin, depending on request/invitation
    private async Task<bool> CanAccept(CircleJoinRequest request)
    {
        return request.FromUserId != request.UpdatedByUserId;
    }

    // CanReject => only the target user or member/admin, depending on request/invitation
    private async Task<bool> CanReject(CircleJoinRequest request)
    {
        return request.FromUserId != request.UpdatedByUserId;
    }

    #endregion
}