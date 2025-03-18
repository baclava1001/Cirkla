using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Mapping.DTOs.CircleRequests;

namespace Cirkla_API.Services.CircleRequests;

public interface ICircleRequestService
{
    Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForCircle(int circleId);
    Task<ServiceResult<IEnumerable<CircleRequest>>> GetAllRequestsForUser(string userId);
    Task<ServiceResult<CircleRequest>> GetRequestById(int id);
    Task<ServiceResult<CircleRequest>> UserRequestsToJoin(CircleRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> UserRequestsToBecomeAdmin(CircleRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> MemberInvitesUser(CircleRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> MembershipInviteFromAdmin(CircleRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> AdminInviteFromAdmin(CircleRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> RejectRequest(int id, CircleRequestUpdateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> RevokeRequest(int id, CircleRequestUpdateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> AdminAcceptsRequest(int id, CircleRequestUpdateDTO circleRequestDTO);
    Task<ServiceResult<CircleRequest>> UserAcceptsInvite(int id, CircleRequestUpdateDTO circleRequestDTO);
}