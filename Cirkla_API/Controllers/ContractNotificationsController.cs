using Cirkla_DAL;
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


        // Add business logic to get only recent notifications, for contracts that are not archived
        [HttpGet]
        public async Task<IEnumerable<ContractNotification>> GetNotifications()
        {
            _logger.LogInformation("Getting all notifications");
            return await _dbContext.ContractNotifications.ToListAsync();
        }


        [HttpPut("markAsRead/{id}")]
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