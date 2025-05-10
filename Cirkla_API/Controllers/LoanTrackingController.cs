using Cirkla_API.Helpers;
using Cirkla_API.Services.TimeLines;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTrackingController(ILoanTrackingService loanTrackingService, ILogger<LoanTrackingController> logger) : ControllerBase
    {
        [HttpGet("contracts/active/borrowers/{userId}")]
        [OpenApiOperation("GetActiveLoansForBorrower")]
        [ProducesResponseType(typeof(IEnumerable<Contract>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoansForBorrower(string userId)
        {
            logger.LogInformation("Getting active contracts for user");
            var result = await loanTrackingService.GetActiveWhereUserIsBorrower(userId);
            // TODO: Return thin and flat DTO:s with bare minimum - each request/contract will be accessible from their details page
            return result.ToHttpResponse();
        }


        [HttpGet("contracts/active/owners({userId}")]
        [OpenApiOperation("GetActiveLoansForOwner")]
        [ProducesResponseType(typeof(IEnumerable<Contract>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoansForOwner(string userId)
        {
            logger.LogInformation("Getting active contracts for user");
            var result = await loanTrackingService.GetActiveWhereUserIsOwner(userId);
            // TODO: Return thin and flat DTO:s with bare minimum - each request/contract will be accessible from their details page
            return result.ToHttpResponse();
        }
    }
}