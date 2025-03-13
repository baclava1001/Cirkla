using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.CircleRequests;

public interface ICircleRequestRepository
{
    Task<CircleRequest> Create(CircleRequest circleRequest);
    Task<IEnumerable<CircleRequest>> GetAll();
    Task<IEnumerable<CircleRequest>> GetAllByCircleId(int circleId);
    Task<IEnumerable<CircleRequest>> GetAllByPendingMemberId(string userId);
    Task<CircleRequest> GetById(int id);
    Task<CircleRequest> Update(CircleRequest circleRequest);
    Task SaveChanges();
}