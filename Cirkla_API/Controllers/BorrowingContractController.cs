using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapping.DTOs.Contracts;
using Cirkla_API.Services;
using NuGet.Protocol;
using Azure;
using Cirkla_DAL.Models;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>BorrowingContracts are how a borrower communicates with an owner of an item</summary>
    public class BorrowingContractController : ControllerBase
    {
        private readonly IBorrowingContractService _borrowingContractService;

        public BorrowingContractController(IBorrowingContractService borrowingContractService)
        {
            _borrowingContractService = borrowingContractService;
        }


        // Ask to borrow = create contract for item owner to review
        [HttpPost("SendRequest")]
        public async Task<ActionResult<Contract>> SendRequest(ContractCreateDTO contractDTOFromClient)
        {
            if(contractDTOFromClient is null)
            {
                return NotFound();
            }

            try
            {
                Contract contract = await _borrowingContractService.SendRequest(contractDTOFromClient);
                // return CreatedAtAction("ViewRequestSummary", new { id = contract.Id }, contract);
                return Ok(contract); // Returns 200 instead of 201 because NSwag
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpGet("ViewRequestSummary{id}")]
        public async Task<ActionResult<Contract>> ViewRequestSummary(int id)
        {
            Contract contract = new();
            try
            {
                contract = await _borrowingContractService.ViewRequestSummary(id);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contract);
        }


        // TODO: Refactor to only patch a date
        [HttpPut("RespondToRequest{id}")]
        public async Task<ActionResult<Contract>> RespondToRequest(int id, ContractReplyDTO contractReplyDTO)
        {
            try
            {
                Contract contract = await _borrowingContractService.RespondToRequest(id, contractReplyDTO);
                return Ok(contract);
            }
            catch(Exception ex)
            {
                return BadRequest($"Failed to update contract: {ex.Message.ToString()}");
            }
        }
    }
}
