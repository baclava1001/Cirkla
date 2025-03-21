using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Mapping.DTOs.CircleJoinRequests;

namespace Cirkla_API.Services.CircleMembership
{
    public partial class CircleMembershipService
    {
        #region Validation helpers

        private async Task<bool> IsExpired(DateTime? expiresAt)
        {
            return (expiresAt < DateTime.Now);
        }

        private async Task<bool> PassesFirstCheck(CircleJoinRequestCreateDTO circleJoinRequestDTO)
        {
            if (circleJoinRequestDTO is null || await IsExpired(circleJoinRequestDTO.ExpiresAt) ||
                circleJoinRequestDTO.Status != CircleRequestStatus.Pending)
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

        // TODO: This might return true for expired or invalid requests, or requests that are persisted even after a user has left the circle
        private async Task<bool> AlreadyInvited(CircleJoinRequest request)
        {
            var requests = await _circleJoinRequestRepository.GetAllByCircleId(request.CircleId);
            return requests.Any(r => r.TargetUserId == request.TargetUserId);
        }

        private async Task<bool> CanJoinAsMember(CircleJoinRequest request)
        {
            return (!request.Circle.Members.Contains(request.TargetUser));
        }

        private async Task<bool> CanInviteMembers(CircleJoinRequest request)
        {
            return (request.Circle.Members.Contains(request.FromUser) ||
                    request.Circle.Administrators.Contains(request.FromUser));
        }

        private async Task<bool> CanInviteAdmin(CircleJoinRequest request)
        {
            return request.Circle.Administrators.Contains(request.FromUser);
        }

        private async Task<bool> IsFromUser(CircleJoinRequest request)
        {
            return (request.FromUserId == request.TargetUserId &&
                    !request.Circle.Members.Contains(request.FromUser));
        }

        private async Task<bool> IsFromMember(CircleJoinRequest request)
        {
            return (request.Circle.Members.Contains(request.FromUser) ||
                    !request.Circle.Administrators.Contains(request.FromUser));
        }

        private async Task<bool> IsFromAdmin(CircleJoinRequest request)
        {
            return request.Circle.Administrators.Contains(request.FromUser);
        }

        private async Task<bool> CanRevoke(CircleJoinRequest request)
        {
            if (request.UpdatedByUserId == null)
            {
                return false;
            }

            return (request.Status == CircleRequestStatus.Pending && request.UpdatedByUserId == request.FromUserId);
        }

        private async Task<bool> CanAccept(CircleJoinRequest request)
        {
            if (request.UpdatedByUserId == null || request.Status != CircleRequestStatus.Pending)
            {
                return false;
            }

            if (await IsFromUser(request))
            {
                // Return true if an admin accepts the request
                return request.Circle.Administrators.Any(a => a.Id == request.UpdatedByUserId);
            }

            if (await IsFromMember(request) || await IsFromAdmin(request))
            {
                // Return true if the target user accepts the request
                return request.TargetUserId == request.UpdatedByUserId;
            }

            return false;
        }

        private async Task<bool> CanReject(CircleJoinRequest request)
        {
            if (request.UpdatedByUserId == null || request.Status != CircleRequestStatus.Pending)
            {
                return false;
            }

            if (await IsFromUser(request))
            {
                // Return true if an admin accepts the request
                return request.Circle.Administrators.Any(a => a.Id == request.UpdatedByUserId);
            }

            if (await IsFromMember(request) || await IsFromAdmin(request))
            {
                // Return true if the target user accepts the request
                return request.TargetUserId == request.UpdatedByUserId;
            }

            return false;
        }

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

        // CanJoin => not already a member (all admins are also members)

        // CanInvite => must be a member or admin

        // CanInviteAdmin => must be an admin


        // Check role of request sender before updating a request

        // CanRevoke => only the user who created the request, while request is still pending

        // CanAccept => only the target user or member/admin, depending on request/invitation

        // CanReject => only the target user or member/admin, depending on request/invitation

        #endregion

        private async Task UpdateCircleMembers(CircleJoinRequest request)
        {
            if (request.Type == CircleJoinRequestType.JoinAsMember)
            {
                request.Circle.Members.Add(request.TargetUser);
                await _circleRepository.UpdateMembers(request.Circle);
            }
        }

        private async Task UpdateCircleAdmins(CircleJoinRequest request)
        {
            if (request.Type == CircleJoinRequestType.JoinAsAdmin)
            {
                request.Circle.Administrators.Add(request.TargetUser);
                await _circleRepository.UpdateAdministrators(request.Circle);
            }
        }
    }
}
