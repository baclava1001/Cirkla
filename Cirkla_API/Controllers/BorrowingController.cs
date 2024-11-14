using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models.Contract;
using Cirkla_API.DTOs.Contracts;
using Cirkla_API.Services;
using Cirkla_API.Helpers;
using NuGet.Protocol;
using Azure;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }


        // Ask to borrow = create contract for item owner to review
        [HttpPost("AskToBorrow")]
        public async Task<ActionResult<Contract>> AskToBorrow(ContractCreateDTO contractDTOFromClient)
        {
            if(contractDTOFromClient is null)
            {
                return NotFound();
            }

            try
            {
                Contract contract = await _borrowingService.AskForItem(contractDTOFromClient);
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
                contract = await _borrowingService.ViewRequestSummary(id);
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
                Contract contract = await _borrowingService.RespondToRequest(id, contractReplyDTO);
                return Ok(contract);
            }
            catch(Exception ex)
            {
                return BadRequest($"Failed to update contract: {ex.Message.ToString()}");
            }
        }
    }
}
