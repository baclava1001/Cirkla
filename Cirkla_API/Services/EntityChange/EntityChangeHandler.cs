using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL;
using Cirkla_DAL.Events;
using Microsoft.AspNetCore.SignalR;

public class EntityChangeHandler
{
    // The IHubContext provides access to the SignalR hub for sending messages to clients.
    private readonly IHubContext<ContractUpdateHub, IContractUpdateHub> _hubContext;
    private readonly AppDbContext _dbContext;

    // Constructor that takes the IHubContext and AppDbContext as parameters.
    public EntityChangeHandler(IHubContext<ContractUpdateHub, IContractUpdateHub> hubContext, AppDbContext dbContext)
    {
        // Assign the IHubContext to the private field.
        _hubContext = hubContext;
        _dbContext = dbContext;

        // Subscribe to the EntityChanged event from the AppDbContext.
        // This means that whenever the EntityChanged event is raised, the OnEntityChanged method will be called.
        _dbContext.EntityChanged += OnEntityChanged;
    }

    // Event handler method that is called when the EntityChanged event is raised.
    // The sender parameter is the source of the event, and the args parameter contains the event data.
    private async void OnEntityChanged(object sender, EntityChangedEventArgs args)
    {
        try
        {
            var notification = new ContractNotification
            {
                Id = args.Entity.Id,
                NotificationMessage = "", // Message is set in the client
                Contract = args.Entity,
                CreatedAt = DateTime.Now,
                HasBeenRead = false
            };

            _dbContext.ContractNotifications.Add(notification);
            await _dbContext.SaveChangesAsync();

            await _hubContext.Clients.All.ReceiveContractUpdate(notification);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Error in OnEntityChanged: {ex.Message}");
        }
    }
}
