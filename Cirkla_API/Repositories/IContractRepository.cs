using Cirkla_DAL.Models.Contract;

namespace Cirkla_API.Repositories
{
    public interface IContractRepository
    {
        Task<Contract> Add(Contract contract);
        Task<IEnumerable<Contract>> GetAllContracts();
        Task<IEnumerable<Contract>> GetAllContracts(string userId);
        Task<Contract> GetContract(int id);
        Task<Contract> Remove(Contract contract);
        Task<Contract> Update(Contract contract);
        Task SaveChanges();
    }
}
