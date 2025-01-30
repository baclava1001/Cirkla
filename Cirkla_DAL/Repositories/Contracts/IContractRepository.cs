using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Contracts
{
    public interface IContractRepository
    {
        Task<Contract> Create(Contract contract);
        Task<IEnumerable<Contract>> GetAll();
        Task<IEnumerable<Contract>> GetIncomingRequestsForInbox(string userId);
        Task<IEnumerable<Contract>> GetUsersPendingRequests(string userId);
        Task<IEnumerable<Contract>> GetUsersAnsweredRequests(string userId);
        Task<IEnumerable<Contract>> GetUsersRequestHistory(string userId);
        Task<IEnumerable<Contract>> GetUsersContractHistory(string userId);
        Task<Contract> GetById(int id);
        Task<Contract> Delete(Contract contract);
        Task<Contract> Update(Contract contract);
        Task SaveChanges();
    }
}
