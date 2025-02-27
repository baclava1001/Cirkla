using Cirkla_DAL;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContractNotificationsController : ControllerBase
    {
        // TODO: Add repository and separate service
        // TODO: Refactor to return ServiceResults instead
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ContractNotificationsController> _logger;

        public ContractNotificationsController(AppDbContext dbContext, ILogger<ContractNotificationsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        // TODO: Remove this? Notifications should be created inside the API and pushed through SignalR, not by the client
        [HttpPost]
        public async Task<IActionResult> CreateNotification(ContractNotification notification)
        {
            _logger.LogInformation("Creating new notification");
            await _dbContext.ContractNotifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync(); 
            return Ok(notification);
        }

        // TODO: Add business logic to get only recent notifications, for contracts that are not archived
        [HttpGet]
        public async Task<IEnumerable<ContractNotification>> GetNotifications()
        {
            
        }


        // TODO: Refactor to PATCH?
        [HttpPut("ToggleMarkAsRead/{id}")]
        public async Task<IActionResult> ToggleMarkAsRead(int id)
        {
            _logger.LogInformation("Marking notification with id: {Id} as read ", id);
            var notification = await _dbContext.ContractNotifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            notification.HasBeenRead = !notification.HasBeenRead;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}   