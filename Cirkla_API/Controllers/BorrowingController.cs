using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models.Contract;
using Cirkla_API.DTOs.Contracts;
using Cirkla_API.Services;
using Cirkla_API.Helpers;

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

        // Ask to borrow = create contract for owner to review
        [HttpPost]
        [ActionName("AskToBorrow")]
        public async Task<ActionResult<Contract>> AskToBorrow(ContractCreateDTO contractDTOFromClient)
        {
            if(contractDTOFromClient is null)
            {
                return NotFound();
            }

            try
            {
                Contract contract = await _borrowingService.AskForItem(contractDTOFromClient);
                return CreatedAtAction("ViewRequestSummary", new { id = contract.Id }, contract);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            //return StatusCode(201, contract);
        }

        [HttpGet("{id}")]
        [ActionName("ViewRequestSummary")]
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
    }
}
