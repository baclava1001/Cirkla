using Cirkla_API.Common.Constants;
using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using FluentValidation.Results;
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

        private async Task<bool> InitialValidation(CircleJoinRequestCreateDTO circleJoinRequestDTO)
        {
            if (circleJoinRequestDTO is null || await IsExpired(circleJoinRequestDTO.ExpiresAt) ||
                circleJoinRequestDTO.Status != CircleRequestStatus.Pending)
            {
                logger.LogError("Invalid request is null, expired or already answered");
                return false;
            }

            return true;
        }

        private async Task<bool> InitialValidation(CircleJoinRequest request)
        {
            if (request is null || await IsExpired(request.ExpiresAt) || request.Status != CircleRequestStatus.Pending)
            {
                logger.LogError("Invalid request is null, expired or already answered");
                return false;
            }

            return true;
        }


        private async Task<bool> AlreadyInvited(CircleJoinRequest request)
        {
            var requests = await circleJoinRequestRepository.GetByTargetUserAndCircle(request.TargetUserId, request.CircleId);
            return (requests.Any(cr => cr.Status is CircleRequestStatus.Pending)) || // Already invited
                   (requests.Any(cr => cr.Circle.Members.Any(m => m == request.TargetUser))); // Already a member
        }

        private async Task<bool> CanJoinAsMember(CircleJoinRequest request)
        {
            return (request.TargetUserId == request.FromUserId &&
                    !request.Circle.Members.Contains(request.TargetUser));
        }

        private async Task<bool> CanInviteMembers(CircleJoinRequest request)
        {
            return (request.FromUserId != request.TargetUserId &&
                    request.Circle.Members.Contains(request.FromUser) ||
                    request.Circle.Administrators.Contains(request.FromUser));
        }

        private async Task<bool> CanInviteAdmin(CircleJoinRequest request)
        {
            return (request.Circle.Administrators.Contains(request.FromUser)
                    && request.FromUserId != request.TargetUserId);
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

        private ServiceResult<T> LogAndReturnValidationError<T>(IEnumerable<ValidationFailure> errors)
        {
            var errorMessages = string.Join(", ", errors.Select(e => e.ErrorMessage));
            logger.LogWarning("Circle join request validation failed: {Errors}", errorMessages);
            return ServiceResult<T>.Fail(errorMessages, ErrorType.ValidationError);
        }


        private async Task HydrateRequestAsync(CircleJoinRequest request)
        {
            request.Circle = await circleRepository.GetById(request.CircleId);
            request.TargetUser = await userRepository.Get(request.TargetUserId);
            request.FromUser = await userRepository.Get(request.FromUserId);
        }


        private async Task<ServiceResult<int>> CreateRequest(CircleJoinRequest request)
        {
            var createdRequest = await circleJoinRequestRepository.Create(request);
            await unitOfWork.SaveChangesWithTransaction();
            // TODO: Send notification to target user
            return ServiceResult<int>.Created(createdRequest.Id);
        }


        private async Task<ServiceResult<object>> UpdateRequest(CircleJoinRequest request, CircleRequestStatus status)
        {
            if (status is CircleRequestStatus.Accepted)
            {
                await AddCircleMembersAndAdmins(request);
            }
            request.Status = status;
            request.UpdatedAt = DateTime.Now;
            await circleJoinRequestRepository.Update(request);
            await unitOfWork.SaveChangesWithTransaction();
            return ServiceResult<object>.Success(null);
        }


        private async Task AddCircleMembersAndAdmins(CircleJoinRequest request)
        {
            if (request.Type == CircleJoinRequestType.JoinAsMember)
            {
                request.Circle.Members.Add(request.TargetUser);
            }
            if (request.Type == CircleJoinRequestType.JoinAsAdmin)
            {
                request.Circle.Administrators.Add(request.TargetUser);
            }
        }
    }
}
