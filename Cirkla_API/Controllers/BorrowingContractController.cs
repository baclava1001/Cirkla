using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapping.DTOs.Contracts;
using NuGet.Protocol;
using Azure;
using Cirkla_API.Helpers;
using Cirkla_DAL.Models;
using Cirkla_API.Services.BorrowingContracts;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>BorrowingContracts are the mediators of communication between a borrower and the owner of an item</summary>
    public class BorrowingContractController : ControllerBase
    {
        private readonly IBorrowingContractService _borrowingContractService;
        private readonly ILogger<BorrowingContractController> _logger;

        public BorrowingContractController(IBorrowingContractService borrowingContractService, ILogger<BorrowingContractController> logger)
        {
            _borrowingContractService = borrowingContractService;
            _logger = logger;
        }


        // Ask to borrow = create contract for item owner to review
        [HttpPost("SendRequest")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendRequest(ContractCreateDTO contractDTO)
        {
            _logger.LogInformation("API received http-request to borrow item (creating new contract)");
            var result = await _borrowingContractService.SendRequest(contractDTO);
            return result.ToHttpResponse();
        }


        [HttpGet("ViewRequestSummary{id}")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ViewRequestSummary(int id)
        {
            _logger.LogInformation("API received http-request for contract details");
            var result = await _borrowingContractService.ViewRequestSummary(id);
            return result.ToHttpResponse();
        }


        // TODO: Refactor to only patch a date instead of put whole object?
        [HttpPut("RespondToRequest{id}")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RespondToRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            _logger.LogInformation("API received http-request to respond 'Accept' or 'Deny' to borrowing request");
            var result = await _borrowingContractService.RespondToRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("CancelRequest{id}")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            _logger.LogInformation("API received http-request to cancel request");
            var result = await _borrowingContractService.CancelRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("ActivateRequest{id}")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActivateRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            _logger.LogInformation("API received http-request to activate a contract");
            var result = await _borrowingContractService.ActivateRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("CompleteRequest{id}")]
        [ProducesResponseType(typeof(ContractResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteRequest(int id, ContractUpdateDTO contractUpdateDTO)
        {
            _logger.LogInformation("API received http-request to complete a contract");
            var result = await _borrowingContractService.CompleteRequest(id, contractUpdateDTO);
            return result.ToHttpResponse();
        }
    }
}
