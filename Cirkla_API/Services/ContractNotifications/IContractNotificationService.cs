using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Mapping.DTOs.ContractNotifications;

namespace Cirkla_API.Services.ContractNotifications;

public interface IContractNotificationService
{
    Task<ServiceResult<ContractNotificationForViews>> CreateNotification(Contract contract);
    Task<ServiceResult<IEnumerable<ContractNotificationForViews>>> GetNotifications();
    Task<ServiceResult<ContractNotificationForViews>> ToggleMarkAsRead(int id);
}