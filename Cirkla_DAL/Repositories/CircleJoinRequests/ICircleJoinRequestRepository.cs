using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.CircleJoinRequests;

public interface ICircleJoinRequestRepository
{
    Task<CircleJoinRequest> Create(CircleJoinRequest circleRequest);
    Task<IEnumerable<CircleJoinRequest>> GetAll();
    Task<IEnumerable<CircleJoinRequest>> GetAllByCircleId(int circleId);
    Task<IEnumerable<CircleJoinRequest>> GetAllByTargetUserId(string userId);
    Task<CircleJoinRequest> GetById(int id);
    Task<CircleJoinRequest> Update(CircleJoinRequest circleRequest);
}