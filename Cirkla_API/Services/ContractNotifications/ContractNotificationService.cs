using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.ContractNotifications;
using Cirkla_DAL.Repositories.UoW;
using Cirkla.Shared.Mappers;
using Cirkla.Shared.DTOs.ContractNotifications;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.ContractNotifications;

public class ContractNotificationService(
    IContractNotificationRepository contractNotificationRepository,
    IUnitOfWork unitOfWork,
    IHubContext<ContractUpdateHub, IContractUpdateClient> hubcontext,
    ILogger<ContractNotificationService> logger)
    : IContractNotificationService
{

    // TODO: Send different notifications depending on user role and contract status
    public async Task<ServiceResult<ContractNotificationForViews>> CreateNotification(Contract contract)
    {
        var notification = new ContractNotification
        {
            NotificationMessage = $"{contract.StatusChanges.LastOrDefault().ChangedBy.FullName} has replied {contract.StatusChanges.Last().To} about {contract.Item.Name}",
            Contract = contract,
            CreatedAt = DateTime.Now,
            HasBeenRead = false
        };
        await contractNotificationRepository.Create(notification);
        await unitOfWork.SaveChanges();
        
        logger.LogInformation("Clients will now be notified.");
        var notificationForView = await Mapper.MapToContractNotificationForViews(notification);
        await hubcontext.Clients.All.ReceiveContractUpdate(notificationForView);
        logger.LogInformation("Clients were notified.");
        return ServiceResult<ContractNotificationForViews>.Success(notificationForView);
    }



    public async Task<ServiceResult<IEnumerable<ContractNotificationForViews>>> GetNotifications()
    {
        logger.LogInformation("Getting all notifications from database");
        var result = await contractNotificationRepository.GetAll();
        if(result is null)
        {
            logger.LogWarning("No notifications found");
            return ServiceResult<IEnumerable<ContractNotificationForViews>>.Fail("No notifications found", ErrorType.NotFound);
        }

        logger.LogInformation("Mapping notifications to client dto:s");
        var notificationsForClient = new List<ContractNotificationForViews>();

        foreach (var notification in result)
        {
            notificationsForClient.Add(await Mapper.MapToContractNotificationForViews(notification));
        }

        return ServiceResult<IEnumerable<ContractNotificationForViews>>.Success(notificationsForClient);
    }



    public async Task<ServiceResult<ContractNotificationForViews>> ToggleMarkAsRead(int id)
    {
        logger.LogInformation("Marking notification with id: {Id} as read/unread", id);
        var notification = await contractNotificationRepository.GetById(id);
        if (notification is null)
        {
            logger.LogWarning("Notification with ID {Id} not found", id);
            return ServiceResult<ContractNotificationForViews>.Fail("Notification not found", ErrorType.NotFound);
        }
        notification.HasBeenRead = !notification.HasBeenRead;

        logger.LogInformation("Toggling read status for notification with ID {Id} and saving to db", id);
        await contractNotificationRepository.Update(notification);
        await unitOfWork.SaveChanges();

        return ServiceResult<ContractNotificationForViews>.Success(await Mapper.MapToContractNotificationForViews(notification));
    }
}