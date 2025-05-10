using Cirkla_API.Helpers;
using Cirkla_API.Services.ContractNotifications;
using Cirkla.Shared.DTOs.ContractNotifications;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Cirkla_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContractNotificationsController(
        IContractNotificationService contractNotificationService,
        ILogger<ContractNotificationsController> logger)
        : ControllerBase
    {
        private readonly ILogger<ContractNotificationsController> _logger = logger;


        // TODO: Add business logic to get only relevant notifications for particular user
        [HttpGet("notifications")]
        [OpenApiOperation("GetNotifications")]
        [ProducesResponseType(typeof(IEnumerable<ContractNotificationForViews>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotifications()
        {
            var result = await contractNotificationService.GetNotifications();
            return result.ToHttpResponse();
        }


        // TODO: Refactor to PATCH?
        [HttpPut("notifications/{id}/toggle")]
        [OpenApiOperation("ToggleNotificationMarkAsRead")]
        [ProducesResponseType(typeof(ContractNotificationForViews), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ToggleMarkAsRead(int id)
        {
            var result = await contractNotificationService.ToggleMarkAsRead(id);
            return result.ToHttpResponse();
        }
    }
}   