using Cirkla_DAL.Models;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Providers;

namespace Cirkla_API.Hubs.ContractUpdate;

// TODO: Add authorization [Authorize] - check https://www.youtube.com/watch?v=O7oaxFgNuYo
public class ContractUpdateHub : Hub<IContractUpdateClient>
{
    public async Task TestConnection()
    {
        Console.WriteLine("Client connected - great success!");
    }
}