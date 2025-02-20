using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL;
using Cirkla_DAL.Events;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace Cirkla_API.Services.EntityChange;

public class EntityChangeHandler : IHostedService
{
    private readonly IHubContext<ContractUpdateHub, IContractUpdateClient> _hubContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EntityChangeHandler> _logger;
    private IServiceScope _scope;
    private AppDbContext _dbContext;

    public EntityChangeHandler(IHubContext<ContractUpdateHub, IContractUpdateClient> hubContext, IServiceProvider serviceProvider, ILogger<EntityChangeHandler> logger)
    {
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _logger.LogInformation("EntityChangeHandler constructor has been called.");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _scope = _serviceProvider.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        _dbContext.EntityChanged += OnEntityChanged;

        _logger.LogInformation("EntityChangeHandler started and event subscribed.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("EntityChangeHandler disposing of DbContext and unsubscribing from event.");
        if (_dbContext != null)
        {
            _dbContext.EntityChanged -= OnEntityChanged;
        }

        _scope?.Dispose();
        _logger.LogInformation("EntityChangeHandler stopped and event unsubscribed.");
        return Task.CompletedTask;
    }

    private async void OnEntityChanged(object? sender, EntityChangedEventArgs args)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                _logger.LogInformation("EntityChanged event received for entity ID: {EntityId}", args.Entity.Id);

                var notification = new ContractNotification
                {
                    Id = args.Entity.Id,
                    NotificationMessage = $"Testing One Two, {args.Entity.Owner.FirstName} has accepted", // TODO: Formulate message here
                    Contract = args.Entity,
                    CreatedAt = DateTime.Now,
                    HasBeenRead = false
                };

                dbContext.ContractNotifications.Add(notification);
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Notification saved for Contract ID: {EntityId}", args.Entity.Id);

                await _hubContext.Clients.All.ReceiveContractUpdate(notification);

                _logger.LogInformation("Notification sent to clients for Contract ID: {EntityId}", args.Entity.Id);
            }
        }
        catch (Exception? ex)
        {
            _logger.LogError(ex, "Error in OnEntityChanged for Contract ID: {EntityId}", args.Entity.Id);
        }
    }
}
