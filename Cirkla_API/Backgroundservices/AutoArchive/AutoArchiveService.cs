﻿using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.UoW;

namespace Cirkla_API.Backgroundservices.AutoArchive
{
    public class AutoArchiveService : IHostedService, IDisposable
    {
        private readonly ILogger<AutoArchiveService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer = null;

        public AutoArchiveService(ILogger<AutoArchiveService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AutoArchiveService started.");

            // Calculate the initial delay until the next 3 AM
            var now = DateTime.Now;
            var nextRunTime = new DateTime(now.Year, now.Month, now.Day, 3, 0, 0, 0);
            if (now > nextRunTime)
            {
                nextRunTime = nextRunTime.AddDays(1);
            }
            var initialDelay = nextRunTime - now;

            _timer = new Timer(DoWork, null, initialDelay, TimeSpan.FromHours(24));
            // _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(90)); // TODO: Comment, for testing only
            return Task.CompletedTask;
        }

        public async void DoWork(object? state)
        {
            _logger.LogInformation("AutoArchiveService attempting to archive contracts.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var _contractRepository = scope.ServiceProvider.GetRequiredService<IContractRepository>();
                var contractsToArchive = await _contractRepository.GetReadyForArchive();

                foreach (var contract in contractsToArchive)
                {
                    contract.StatusChanges.Add(new ContractStatusChange
                    {
                        ChangedAt = DateTime.Now,
                        ChangedBy = null,
                        Contract = contract,
                        From = contract.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To,
                        To = ContractStatus.Archived
                    });
                    await _contractRepository.Update(contract);
                    await _unitOfWork.SaveChangesWithTransaction();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AutoArchiveService stopped.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}
