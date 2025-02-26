using Cirkla_DAL.Models;

namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateClient
{
    Task ReceiveContractUpdate(ContractNotification contractNotification);
}