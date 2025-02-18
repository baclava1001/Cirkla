namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateHub
{
    Task ReceiveContractUpdate(string message);
}