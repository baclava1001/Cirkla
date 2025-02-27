using Cirkla_API.Helpers;
using Cirkla_API.Services.ContractNotifications;
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
        private readonly IContractNotificationService _contractNotificationService;
        private readonly ILogger<ContractNotificationsController> _logger;

        public ContractNotificationsController(IContractNotificationService contractNotificationService, ILogger<ContractNotificationsController> logger)
        {
            _contractNotificationService = contractNotificationService;
            _logger = logger;
        }


        // TODO: Add business logic to get only recent notifications, for contracts that are not archived
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            _logger.LogInformation("Fetching notifications from API");
            var result = await _contractNotificationService.GetNotifications();
            return result.ToHttpResponse();
        }


        // TODO: Refactor to PATCH?
        [HttpPut("ToggleMarkAsRead/{id}")]
        public async Task<IActionResult> ToggleMarkAsRead(int id)
        {
            _logger.LogInformation("Marking notification with id: {Id} as read ", id);
            var result = await _contractNotificationService.ToggleMarkAsRead(id);
            return result.ToHttpResponse();
        }
    }
}   