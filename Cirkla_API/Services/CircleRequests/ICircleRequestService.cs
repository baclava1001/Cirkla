using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.CircleRequests;

public interface ICircleRequestService
{
    Task<ServiceResult<CircleRequest>> UserRequestsToJoin(CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> UserRequestsToBecomeAdmin(CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> MemberInvitesUser(CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> MembershipInviteFromAdmin(CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> AdminInviteFromAdmin(CircleRequest circleRequest);
    Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForCircle(int circleId);
    Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForUser(string userId);
    Task<ServiceResult<CircleRequest>> GetRequestById(int id);
    Task<ServiceResult<CircleRequest>> RejectRequest(int id, CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> RevokeRequest(int id, CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> AdminAcceptsRequest(int id, CircleRequest circleRequest);
    Task<ServiceResult<CircleRequest>> UserAcceptsInvite(int id, CircleRequest circleRequest);
}