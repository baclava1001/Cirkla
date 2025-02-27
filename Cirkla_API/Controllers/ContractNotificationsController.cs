using Cirkla_API.Helpers;
using Cirkla_API.Services.ContractNotifications;
using Mapping.DTOs.ContractNotifications;
using Microsoft.AspNetCore.Mvc;

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


        // TODO: Add business logic to get only relevant notifications for particular user
        [HttpGet("GetNotifications")]
        [ProducesResponseType(typeof(IEnumerable<ContractNotificationForViews>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotifications()
        {
            _logger.LogInformation("Fetching notifications from API");
            var result = await _contractNotificationService.GetNotifications();
            return result.ToHttpResponse();
        }


        // TODO: Refactor to PATCH?
        [HttpPut("ToggleMarkAsRead/{id}")]
        [ProducesResponseType(typeof(ContractNotificationForViews), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ToggleMarkAsRead(int id)
        {
            _logger.LogInformation("Marking notification with id: {Id} as read ", id);
            var result = await _contractNotificationService.ToggleMarkAsRead(id);
            return result.ToHttpResponse();
        }
    }
}   