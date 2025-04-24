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

        private async Task<bool> InitialValidation(CircleJoinRequestCreateDTO requestDTO)
        {
            if (requestDTO is null || await IsExpired(requestDTO.ExpiresAt) ||
                requestDTO.Status != CircleRequestStatus.Pending)
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
            request.UpdatedByUser = await userRepository.Get(request.UpdatedByUserId);
        }


        private async Task<ServiceResult<int>> CreateRequest(CircleJoinRequest request)
        {
            var createdRequest = await circleJoinRequestRepository.Create(request);
            await unitOfWork.SaveChangesWithTransaction();
            // TODO: Send notification to target user
            return ServiceResult<int>.Created(createdRequest.Id);
        }


        private async Task<ServiceResult<CircleJoinRequest>> UpdateRequest(CircleJoinRequest request, CircleRequestStatus status)
        {
            if (status is CircleRequestStatus.Accepted)
            {
                await AddCircleMembersAndAdmins(request);
            }
            request.Status = status;
            request.UpdatedAt = DateTime.Now;
            await circleJoinRequestRepository.Update(request);
            await unitOfWork.SaveChangesWithTransaction();
            return ServiceResult<CircleJoinRequest>.Success(request);
        }


        private async Task AddCircleMembersAndAdmins(CircleJoinRequest request)
        {
            if (request.Type == CircleJoinRequestType.JoinAsMember)
            {
                request.Circle.Members.Add(request.TargetUser);
                await circleRepository.UpdateAdministrators(request.Circle);
            }
            if (request.Type == CircleJoinRequestType.JoinAsAdmin)
            {
                request.Circle.Administrators.Add(request.TargetUser);
                await circleRepository.UpdateAdministrators(request.Circle);
            }
        }
    }
}
