using Cirkla_API.Helpers;
using Cirkla_API.Services.TimeLines;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLineController : ControllerBase
    {
        private readonly ITimeLineService _timeLineService;
        private readonly ILogger<TimeLineController> _logger;

        public TimeLineController(ITimeLineService timeLineService, ILogger<TimeLineController> logger)
        {
            _timeLineService = timeLineService;
            _logger = logger;
        }


        [HttpGet("BorrowingTimeLine")]
        [ProducesResponseType(typeof(IEnumerable<Contract>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTimeLineForBorrowing(string userId)
        {
            _logger.LogInformation("Getting active contracts for user");
            var result = await _timeLineService.GetActiveWhereUserIsBorrower(userId);
            // TODO: Return thin and flat DTO:s with bare minimum - each request/contract will be accessible from their details page
            return result.ToHttpResponse();
        }


        [HttpGet("SharingTimeLine")]
        [ProducesResponseType(typeof(IEnumerable<Contract>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTimeLineForSharing(string userId)
        {
            _logger.LogInformation("Getting active contracts for user");
            var result = await _timeLineService.GetActiveWhereUserIsOwner(userId);
            // TODO: Return thin and flat DTO:s with bare minimum - each request/contract will be accessible from their details page
            return result.ToHttpResponse();
        }
    }
}