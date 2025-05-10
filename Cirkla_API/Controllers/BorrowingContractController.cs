using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla.Shared.DTOs.Contracts;
using NuGet.Protocol;
using Azure;
using Cirkla_API.Helpers;
using Cirkla_DAL.Models;
using Cirkla_API.Services.BorrowingContracts;
using NSwag.Annotations;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>BorrowingContracts are the mediators of communication between a borrower and the owner of an item</summary>
    public class BorrowingContractController(IBorrowingContractService borrowingContractService,
                                            ILogger<BorrowingContractController> logger) : ControllerBase
    {
        // Send request to borrow = create contract for item owner to review
        [HttpPost("requests")]
        [OpenApiOperation("SendBorrowingRequest")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendRequest(ContractCreateDTO contractDTO)
        {
            logger.LogInformation("API received http-request to borrow item (creating new contract)");
            var result = await borrowingContractService.SendRequest(contractDTO);
            return result.ToHttpResponse();
        }


        [HttpGet("requests/{id}")]
        [OpenApiOperation("ViewBorrowingRequestSummary")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ViewRequestSummary(int id)
        {
            logger.LogInformation("API received http-request for contract details");
            var result = await borrowingContractService.ViewRequestSummary(id);
            return result.ToHttpResponse();
        }

        [HttpGet("items/{itemId}/requests")]
        [OpenApiOperation("GetAllBorrowingRequestsForItem")]
        [ProducesResponseType(typeof(IEnumerable<ContractResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequests(int itemId)
        {
            logger.LogInformation("API received http-request for all requests for an item");
            var result = await borrowingContractService.GetActiveForItem(itemId);
            return result.ToHttpResponse();
        }


        [HttpPut("requests/{id}/respond")]
        [OpenApiOperation("RespondToBorrowingRequest")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RespondToRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            logger.LogInformation("API received http-request to respond 'Accept' or 'Deny' to borrowing request");
            var result = await borrowingContractService.RespondToRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("requests/{id}/cancel")]
        [OpenApiOperation("CancelBorrowingRequest")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            logger.LogInformation("API received http-request to cancel request");
            var result = await borrowingContractService.CancelRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("requests/{id}/activate")]
        [OpenApiOperation("ActivateBorrowingRequest")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            logger.LogInformation("API received http-request to activate a contract");
            var result = await borrowingContractService.ActivateRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("requests/{id}/complete")]
        [OpenApiOperation("CompleteBorrowingRequest")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            logger.LogInformation("API received http-request to complete a contract");
            var result = await borrowingContractService.CompleteRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }
    }
}
