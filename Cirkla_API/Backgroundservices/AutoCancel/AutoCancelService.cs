using Cirkla_API.Backgroundservices.AutoLate;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.UoW;

namespace Cirkla_API.Backgroundservices.AutoCancel;

public class AutoCancelService : IHostedService, IDisposable
{
    private readonly ILogger<AutoLateService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer = null;

    public AutoCancelService(ILogger<AutoLateService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("AutoCancelService started.");
        // Calculate the initial delay until the next 3 AM
        var now = DateTime.Now;
        var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 3, 0, 0, 0);
        if (now > nextRunTime)
        {
            nextRunTime = nextRunTime.AddDays(1);
        }
        var initialDelay = nextRunTime - now;
        _timer = new Timer(DoWork, null, initialDelay, TimeSpan.FromHours(24));
        // _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30)); // TODO: Comment, for testing only
        return Task.CompletedTask;
    }

    public async void DoWork(object? state)
    {
        _logger.LogInformation("AutoCancelService attempting to cancel contracts.");
        using (var scope = _serviceProvider.CreateScope())
        {
            var _contractRepository = scope.ServiceProvider.GetRequiredService<IContractRepository>();
            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var contractsToCancel = await _contractRepository.GetAcceptedButNotPickedUp();
            foreach (var contract in contractsToCancel)
            {
                contract.StatusChanges.Add(new ContractStatusChange
                {
                    ChangedAt = DateTime.Now,
                    ChangedBy = null,
                    Contract = contract,
                    From = contract.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To,
                    To = ContractStatus.Cancelled
                });
                await _contractRepository.Update(contract);
                await _unitOfWork.SaveChangesWithTransaction();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("AutoCancelService stopped.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose() => _timer?.Dispose();
}