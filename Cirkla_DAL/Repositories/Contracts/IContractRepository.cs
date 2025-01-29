using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Contracts
{
    public interface IContractRepository
    {
        Task<Contract> Add(Contract contract);
        Task<IEnumerable<Contract>> GetAllContracts();
        Task<IEnumerable<Contract>> GetIncomingRequestsForInbox(string userId);
        Task<IEnumerable<Contract>> GetUsersPendingRequests(string userId);
        Task<IEnumerable<Contract>> GetUsersAnsweredRequests(string userId);
        Task<IEnumerable<Contract>> GetUsersRequestHistory(string userId);
        Task<IEnumerable<Contract>> GetUsersContractHistory(string userId);
        Task<Contract> GetContract(int id);
        Task<Contract> Remove(Contract contract);
        Task<Contract> Update(Contract contract);
        Task SaveChanges();
    }
}
