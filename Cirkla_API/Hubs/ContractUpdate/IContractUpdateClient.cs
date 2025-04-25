using Cirkla_DAL.Models;
using Cirkla.Shared.DTOs.ContractNotifications;

namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateClient
{
    Task ReceiveContractUpdate(ContractNotificationForViews contractNotificationForView);
}