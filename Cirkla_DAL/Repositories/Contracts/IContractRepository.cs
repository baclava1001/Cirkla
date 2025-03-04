using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Contracts
{
    public interface IContractRepository
    {
        Task<Contract> Create(Contract contract);
        Task<IEnumerable<Contract>> GetAll();
        Task<IEnumerable<Contract>> GetActiveWhereUserIsBorrower(string userId);
        Task<IEnumerable<Contract>> GetActiveWhereUserIsOwner(string userId);
        Task<IEnumerable<Contract>> GetArchivedWhereUsersWasBorrower(string userId);
        Task<IEnumerable<Contract>> GetArchivedWhereUserWasOwner(string userId);
        Task<IEnumerable<Contract>> GetActiveForItem(int itemId);
        Task<Contract?> GetById(int id);
        Task<Contract?> Delete(Contract contract);
        Task<Contract?> Update(Contract contract);
        Task SaveChanges();
    }
}
