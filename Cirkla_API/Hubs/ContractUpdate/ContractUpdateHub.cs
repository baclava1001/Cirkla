using Cirkla_DAL.Events;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Providers;

namespace Cirkla_API.Hubs.ContractUpdate;

// TODO: Add authorization [Authorize] - check https://www.youtube.com/watch?v=O7oaxFgNuYo
public class ContractUpdateHub : Hub<IContractUpdateHub>
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    // Send updates to clients
    public async Task HandleEntityChanged(EntityChangedEventArgs args)
    {
        var entity = args.Entity;
        await Clients.All.ReceiveContractUpdate(entity);
    }
}
