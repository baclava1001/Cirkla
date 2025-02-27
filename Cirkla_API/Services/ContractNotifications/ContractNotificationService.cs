using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.ContractNotifications;
using Mapping.DTOs.ContractNotifications;
using Mapping.Mappers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.ContractNotifications;

public class ContractNotificationService : IContractNotificationService
{
    private readonly IContractNotificationRepository _contractNotificationRepository;
    private readonly IHubContext<ContractUpdateHub, IContractUpdateClient> _hubContext;
    private readonly ILogger<ContractNotificationService> _logger;

    public ContractNotificationService(
        IContractNotificationRepository contractNotificationRepository,
        IHubContext<ContractUpdateHub, IContractUpdateClient> hubcontext,
        ILogger<ContractNotificationService> logger)
    {
        _contractNotificationRepository = contractNotificationRepository;
        _hubContext = hubcontext;
        _logger = logger;
    }


    // TODO: Send different notifications depending on user role and contract status
    public async Task<ServiceResult<ContractNotificationForViews>> CreateNotification(Contract contract)
    {
        var notification = new ContractNotification
        {
            NotificationMessage = $"Test message: {contract.Owner.FirstName} has replied {contract.StatusChanges.Last().To}",
            Contract = contract,
            CreatedAt = DateTime.Now,
            HasBeenRead = false
        };
        await _contractNotificationRepository.Create(notification);
        await _contractNotificationRepository.SaveChanges();
        try
        {
            _logger.LogInformation("Clients will now be notified.");
            var notificationForView = await Mapper.MapToContractNotificationForViews(notification);
            await _hubContext.Clients.All.ReceiveContractUpdate(notificationForView);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hub failed to send contract update notification to clients");
        }
    }

    public async Task<ServiceResult<IEnumerable<ContractNotificationForViews>>> GetNotifications()
    {
        _logger.LogInformation("Getting all notifications from database");
        var result = await _contractNotificationRepository.GetAll();
        if(result is null)
        {
            _logger.LogWarning("No notifications found");
            return ServiceResult<IEnumerable<ContractNotificationForViews>>.Fail("No notifications found", ErrorType.NotFound);
        }

        _logger.LogInformation("Mapping notifications to client dto:s");
        var notificationsForClient = new List<ContractNotificationForViews>();
        try
        {
            foreach (var notification in result)
            {
                notificationsForClient.Add(await Mapper.MapToContractNotificationForViews(notification));
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unexpected error mapping notifications to client dto:s");
            return ServiceResult<IEnumerable<ContractNotificationForViews>>.Fail("Internal server error", ErrorType.InternalError);
        }
        return ServiceResult<IEnumerable<ContractNotificationForViews>>.Success(notificationsForClient);
    }


    public async Task<ServiceResult<ContractNotificationForViews>> ToggleMarkAsRead(int id)
    {
        _logger.LogInformation("Marking notification with id: {Id} as read/unread", id);
        var notification = await _contractNotificationRepository.GetById(id);
        if (notification is null)
        {
            _logger.LogWarning("Notification with ID {Id} not found", id);
            return ServiceResult<ContractNotificationForViews>.Fail("Notification not found", ErrorType.NotFound);
        }
        notification.HasBeenRead = !notification.HasBeenRead;

        try
        {
            _logger.LogInformation("Toggling read status for notification with ID {Id} and saving to db", id);
            await _contractNotificationRepository.Update(notification);
            await _contractNotificationRepository.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error toggling read status for notification with ID {Id}", id);
            return ServiceResult<ContractNotificationForViews>.Fail("Internal server error", ErrorType.InternalError);
        }
        return ServiceResult<ContractNotificationForViews>.Success(await Mapper.MapToContractNotificationForViews(notification));
    }
}