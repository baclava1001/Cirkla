using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.CircleRequests;
using Cirkla_DAL.Repositories.Circles;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.CircleRequests;

public class CircleRequestService : ICircleRequestService
{
    private readonly ICircleRequestRepository _circleRequestRepository;
    private readonly ICircleRepository _circleRepository;
    private readonly ILogger<CircleRequestService> _logger;

    public CircleRequestService(ICircleRequestRepository circleRequestRepository, ILogger<CircleRequestService> logger, ICircleRepository circleRepository)
    {
        _circleRequestRepository = circleRequestRepository;
        _logger = logger;
        _circleRepository = circleRepository;
    }


    public async Task<ServiceResult<CircleRequest>> UserRequestsToJoin(CircleRequest circleRequest)
    {
        // TODO: Check if user is already a member or admin
        if (circleRequest.RequestType is not CircleJoinRequestType.UserRequestToJoin ||
            circleRequest.FromUser.Id != circleRequest.PendingMember.Id)
        {
            _logger.LogError("Invalid request for user with ID {UserId} to join circle with ID {CircleId}", circleRequest.FromUser.Id, circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            // TODO: Refactor to DTO:s and use mapping
            var requestToDb = new CircleRequest
            {
                CircleId = circleRequest.CircleId,
                PendingMemberId = circleRequest.PendingMemberId,
                FromUserId = circleRequest.FromUserId,
                RequestType = circleRequest.RequestType,
                RequestDate = circleRequest.RequestDate,
                Status = circleRequest.Status,
                ExpiresAt = circleRequest.ExpiresAt
            };
            var createdRequest = await _circleRequestRepository.Create(requestToDb);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to circle admins
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> UserRequestsToBecomeAdmin(CircleRequest circleRequest)
    {
        if (circleRequest.RequestType is not CircleJoinRequestType.UserRequestToBecomeAdmin ||
            circleRequest.FromUser.Id != circleRequest.PendingMember.Id)
        {
            _logger.LogError("Invalid request type for admin request to join circle with {Id}",
                circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            var createdRequest = await _circleRequestRepository.Create(circleRequest);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to circle admins
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> MemberInvitesUser(CircleRequest circleRequest)
    {
        if (circleRequest.RequestType is not CircleJoinRequestType.MemberInviteToUser ||
            circleRequest.FromUser.Id == circleRequest.PendingMember.Id)
        {
            _logger.LogError("Invalid invite to user with {UserId} for circle with {CircleId}", circleRequest.PendingMember.Id, circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }
        try
        {
            var createdRequest = await _circleRequestRepository.Create(circleRequest);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
            // TODO: Send notification to invited user
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> MembershipInviteFromAdmin(CircleRequest circleRequest)
    {
        if (circleRequest.RequestType is not CircleJoinRequestType.MembershipInviteFromAdmin ||
            !circleRequest.Circle.Administrators.Any(admin => admin.Id == circleRequest.FromUser.Id))
        {
            _logger.LogError("Invalid membership invite from admin with {UserId} for circle with {CircleId}", circleRequest.PendingMember.Id, circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }
            
        try
        {
            var createdRequest = await _circleRequestRepository.Create(circleRequest);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<CircleRequest>> AdminInviteFromAdmin(CircleRequest circleRequest)
    {
        if (circleRequest.RequestType is not CircleJoinRequestType.AdminInviteFromAdmin ||
            !circleRequest.Circle.Administrators.Any(admin => admin.Id == circleRequest.FromUser.Id))
        {
            _logger.LogError("Invalid admin invite from admin with {UserId} for circle with {CircleId}", circleRequest.PendingMember.Id, circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Invalid request type", ErrorType.ValidationError);
        }

        try
        {
            var createdRequest = await _circleRequestRepository.Create(circleRequest);
            await _circleRequestRepository.SaveChanges();
            return ServiceResult<CircleRequest>.Created(createdRequest);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating request for circle with {Id}", circleRequest.Circle.Id);
            return ServiceResult<CircleRequest>.Fail("Error creating request", ErrorType.InternalError);
        }
    }


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

    public async Task<ServiceResult<CircleRequest>> RejectRequest(int id, CircleRequest circleRequest)
    {
        if (circleRequest is null ||
            circleRequest.Id != id ||
            circleRequest.Status is not CircleRequestStatus.Pending ||
            circleRequest.UpdatedByUser.Id == circleRequest.FromUser.Id)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var request = await _circleRequestRepository.Update(circleRequest);
            await _circleRequestRepository.SaveChanges();
            if (request == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(circleRequest);
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

    public async Task<ServiceResult<CircleRequest>> RevokeRequest(int id, CircleRequest circleRequest)
    {
        if (circleRequest is null ||
            circleRequest.Id != id ||
            circleRequest.Status is not CircleRequestStatus.Pending ||
            circleRequest.UpdatedByUser.Id != circleRequest.FromUser.Id)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }

        try
        {
            var request = await _circleRequestRepository.Update(circleRequest);
            await _circleRequestRepository.SaveChanges();
            if (request == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(circleRequest);
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

    public async Task<ServiceResult<CircleRequest>> AdminAcceptsRequest(int id, CircleRequest circleRequest)
    {
        if (circleRequest is null ||
            circleRequest.Id != id ||
            circleRequest.Status is not CircleRequestStatus.Pending ||
            circleRequest.UpdatedByUser.Id == circleRequest.FromUser.Id)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        try
        {
            await _circleRepository.UpdateMembers(circleRequest.Circle);
            await _circleRepository.UpdateAdministrators(circleRequest.Circle);
            var request = await _circleRequestRepository.Update(circleRequest);
            await _circleRequestRepository.SaveChanges();
            if (request == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(circleRequest);
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

    public async Task<ServiceResult<CircleRequest>> UserAcceptsInvite(int id, CircleRequest circleRequest)
    {
        if (circleRequest is null ||
            circleRequest.Id != id ||
            circleRequest.Status is not CircleRequestStatus.Pending ||
            circleRequest.PendingMember.Id == circleRequest.FromUser.Id)
        {
            _logger.LogError("Invalid request with {Id}", id);
            return ServiceResult<CircleRequest>.Fail("Invalid request", ErrorType.ValidationError);
        }
        try
        {
            await _circleRepository.UpdateMembers(circleRequest.Circle);
            await _circleRepository.UpdateAdministrators(circleRequest.Circle);
            var request = await _circleRequestRepository.Update(circleRequest);
            await _circleRequestRepository.SaveChanges();
            if (request == null)
            {
                return ServiceResult<CircleRequest>.Fail("Request not found", ErrorType.NotFound);
            }
            return ServiceResult<CircleRequest>.Success(circleRequest);
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
}
