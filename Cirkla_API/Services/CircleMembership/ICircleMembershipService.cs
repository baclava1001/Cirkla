using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Mapping.DTOs.CircleJoinRequests;

namespace Cirkla_API.Services.CircleMembership;

public interface ICircleMembershipService
{
    Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForCircle(int circleId);
    Task<ServiceResult<IEnumerable<CircleJoinRequest>>> GetAllRequestsForUser(string userId);
    Task<ServiceResult<CircleJoinRequest>> GetRequestById(int id);
    Task<ServiceResult<CircleJoinRequest>> SendJoinRequest(CircleJoinRequestCreateDTO circleRequestDTO);
    Task<ServiceResult<CircleJoinRequest>> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO);
    Task<ServiceResult<CircleJoinRequest>> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO);
    Task<ServiceResult<CircleJoinRequest>> AcceptRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO);
}