using Cirkla_API.Hubs.ContractUpdate;
using Microsoft.AspNetCore.SignalR;

namespace Cirkla_API.Services.Notifications;

public class ServerTimeNotifier : BackgroundService
{
    private static readonly TimeSpan _interval = TimeSpan.FromSeconds(10);
    private readonly ILogger<ServerTimeNotifier> _logger;
    private readonly IHubContext<ContractUpdateHub, IContractUpdateHub> _hubContext;
    public ServerTimeNotifier(ILogger<ServerTimeNotifier> logger, IHubContext<ContractUpdateHub, IContractUpdateHub> hubContext)
    {
        _logger = logger;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(_interval);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("{Service} ran on {Time}.", nameof(ServerTimeNotifier), DateTime.Now);
            await _hubContext.Clients.All.ReceiveContractUpdate();
        }
    }
}