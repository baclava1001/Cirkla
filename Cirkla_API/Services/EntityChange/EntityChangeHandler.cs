using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL;
using Cirkla_DAL.Events;
using Microsoft.AspNetCore.SignalR;

public class EntityChangeHandler
{
    // The IHubContext provides access to the SignalR hub for sending messages to clients.
    private readonly IHubContext<ContractUpdateHub, IContractUpdateHub> _hubContext;

    // Constructor that takes the IHubContext and AppDbContext as parameters.
    public EntityChangeHandler(IHubContext<ContractUpdateHub, IContractUpdateHub> hubContext, AppDbContext dbContext)
    {
        // Assign the IHubContext to the private field.
        _hubContext = hubContext;

        // Subscribe to the EntityChanged event from the AppDbContext.
        // This means that whenever the EntityChanged event is raised, the OnEntityChanged method will be called.
        dbContext.EntityChanged += OnEntityChanged;
    }

    // Event handler method that is called when the EntityChanged event is raised.
    // The sender parameter is the source of the event, and the args parameter contains the event data.
    private async void OnEntityChanged(object sender, EntityChangedEventArgs args)
    {
        // Use the IHubContext to send the updated Contract entity to all connected clients.
        // The ReceiveContractUpdate method on the client side will be called with the updated Contract entity.
        await _hubContext.Clients.All.ReceiveContractUpdate(args.Entity);
    }
}