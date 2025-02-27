using Cirkla_DAL.Models;
using Mapping.DTOs.ContractNotifications;

namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateClient
{
    Task ReceiveContractUpdate(ContractNotificationForViews contractNotificationForView);
}