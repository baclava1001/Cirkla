using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Providers;

namespace Cirkla_API.Hubs.ContractUpdate;

// TODO: Add authorization [Authorize] - check https://www.youtube.com/watch?v=O7oaxFgNuYo
public class ContractUpdateHub : Hub<IContractUpdateHub>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveContractUpdate($"{Context.ConnectionId} has joined the hub!");
        await base.OnConnectedAsync();
    }
}