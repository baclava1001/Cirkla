using Cirkla.API.Common;
using Cirkla.Data.Models;
using Cirkla.Shared.DTOs.ContractNotifications;

namespace Cirkla.API.Services.ContractNotifications;

public interface IContractNotificationService
{
    Task<ServiceResult<ContractNotificationForViews>> CreateNotification(Contract contract);
    Task<ServiceResult<IEnumerable<ContractNotificationForViews>>> GetNotifications();
    Task<ServiceResult<ContractNotificationForViews>> ToggleMarkAsRead(int id);
}