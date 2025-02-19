using Cirkla_DAL.Models;

namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateHub
{
    Task ReceiveContractUpdate(Contract contract);
}