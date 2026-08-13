using Cirkla.Data.Models;
using Cirkla.Shared.DTOs.ContractNotifications;

namespace Cirkla.API.Hubs.ContractUpdate;

public interface IContractUpdateClient
{
    Task ReceiveContractUpdate(ContractNotificationForViews contractNotificationForView);
}