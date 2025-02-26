using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.ContractNotifications;

public interface IContractNotificationRepository
{
    Task<ContractNotification> Create(ContractNotification contractNotification);
    Task<IEnumerable<ContractNotification>> GetAll();
    Task<ContractNotification?> GetById(int id);
    Task<ContractNotification?> Update(ContractNotification contractNotification);
    Task<ContractNotification?> Delete(ContractNotification contractNotification);
    Task SaveChanges();
}