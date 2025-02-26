using Cirkla_DAL.Models;
using System.Text.Json;

namespace Cirkla_API.Hubs.ContractUpdate;

public interface IContractUpdateClient
{
    Task ReceiveContractUpdate(string contractNotification);
}