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
            return await CreateAdminRequest(requestToDb);
        }
        return ServiceResult<CircleJoinRequest>.Fail("Invalid request type", ErrorType.ValidationError);
    }

    public async Task<ServiceResult<CircleJoinRequest>> CreateMembershipRequest(CircleJoinRequest request)
    {
        if (!await CanJoinAsMember(request))
        {
            _logger.LogError("Invalid request to add member with ID {UserId} to circle with ID {CircleId}", request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        if (!await CanInviteMembers(request))
        {
            _logger.LogError("User with ID {UpdatingUser} not authorized to invite member with ID {UserId} to circle with ID {CircleId}.", request.FromUserId, request.TargetUserId, request.CircleId);
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
            _logger.LogError("User with ID {UpdatingUser} not authorized to add member with ID {UserId} to circle with ID {CircleId}.", request.FromUserId, request.TargetUserId, request.CircleId);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        var createdRequest = await _circleJoinRequestRepository.Create(request);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Created(createdRequest);
    }

    #endregion


    #region Updating

    public async Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanRevoke(existingRequest) || existingRequest.CircleId != circleRequestDTO.CircleId)
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

    public async Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanReject(existingRequest) || existingRequest.CircleId != circleRequestDTO.CircleId)
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


    public async Task<ServiceResult<CircleJoinRequest>> AcceptRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
    {
        if (circleRequestDTO.Id != id)
        {
            _logger.LogError("Invalid request update with mismatching id:s");
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        var existingRequest = await _circleJoinRequestRepository.GetById(id);
        existingRequest.UpdatedByUserId = circleRequestDTO.UpdatedByUserId;

        if (!await PassesFirstCheck(existingRequest) || !await CanAccept(existingRequest) || existingRequest.CircleId != circleRequestDTO.CircleId)
        {
            _logger.LogError("Invalid request update with ID {Id}", id);
            return ServiceResult<CircleJoinRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        await UpdateCircleMembersAndAdmins(existingRequest);
        existingRequest.Status = CircleRequestStatus.Accepted;
        existingRequest.UpdatedAt = DateTime.Now;
        var updatedRequest = await _circleJoinRequestRepository.Update(existingRequest);
        await _circleJoinRequestRepository.SaveChanges();
        return ServiceResult<CircleJoinRequest>.Success(updatedRequest);
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
    private async Task<bool> IsFromUser(CircleJoinRequest request)
    {
        return (request.FromUserId == request.TargetUserId &&
                !request.Circle.Members.Contains(request.FromUser));
    }

    private async Task<bool> IsFromMember(CircleJoinRequest request)
    {
        return (request.Circle.Members.Contains(request.FromUser) || !request.Circle.Administrators.Contains(request.FromUser));
    }

    private async Task<bool> IsFromAdmin(CircleJoinRequest request)
    {
        return request.Circle.Administrators.Contains(request.FromUser);
    }

    // CanRevoke => only the user who created the request, while request is still pending
    private async Task<bool> CanRevoke(CircleJoinRequest request)
    {
        if (request.UpdatedByUserId == null)
        {
            return false;
        }
        return (request.Status == CircleRequestStatus.Pending && request.UpdatedByUserId == request.FromUserId);
    }

    // CanAccept => only the target user or member/admin, depending on request/invitation
    private async Task<bool> CanAccept(CircleJoinRequest request)
    {
        if (request.UpdatedByUserId == null)
        {
            return false;
        }
        return request.FromUserId != request.UpdatedByUserId;
    }

    // CanReject => only the target user or member/admin, depending on request/invitation
    private async Task<bool> CanReject(CircleJoinRequest request)
    {
        if (request.UpdatedByUserId == null)
        {
            return false;
        }
        return request.FromUserId != request.UpdatedByUserId;
    }


    private async Task UpdateCircleMembersAndAdmins(CircleJoinRequest request)
    {
        if (request.Type == CircleJoinRequestType.JoinAsMember)
        {
            request.Circle.Members.Add(request.TargetUser);
            await _circleRepository.UpdateMembers(request.Circle);
        }
        if (request.Type == CircleJoinRequestType.JoinAsAdmin)
        {
            request.Circle.Administrators.Add(request.TargetUser);
            await _circleRepository.UpdateAdministrators(request.Circle);
        }
    }

    #endregion
}