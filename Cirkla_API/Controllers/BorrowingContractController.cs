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

    /// <summary>BorrowingContracts are how a borrower communicates with an owner of an item</summary>
    public class BorrowingContractController : ControllerBase
    {
        private readonly IBorrowingContractService _borrowingContractService;
        private readonly ILogger _logger;

        public BorrowingContractController(IBorrowingContractService borrowingContractService, ILogger logger)
        {
            _borrowingContractService = borrowingContractService;
            _logger = logger;
        }


        // Ask to borrow = create contract for item owner to review
        [HttpPost("SendRequest")]
        public async Task<IActionResult> SendRequest(ContractCreateDTO contractDTOFromClient)
        {
            _logger.LogInformation("Sending request to borrow item (creating new contract)");
            var result = await _borrowingContractService.SendRequest(contractDTOFromClient);
            return result.ToHttpResponse();
        }


        [HttpGet("ViewRequestSummary{id}")]
        public async Task<IActionResult> ViewRequestSummary(int id)
        {
            _logger.LogInformation("Fetching request summary");
            var result = await _borrowingContractService.ViewRequestSummary(id);
            return result.ToHttpResponse();
        }


        // TODO: Refactor to only patch a date instead of put whole object?
        [HttpPut("RespondToRequest{id}")]
        public async Task<IActionResult> RespondToRequest(int id, ContractReplyDTO contractReplyDTO)
        {
            _logger.LogInformation("Responding to request");
            var result = await _borrowingContractService.RespondToRequest(id, contractReplyDTO);
            return result.ToHttpResponse();
        }
    }
}
