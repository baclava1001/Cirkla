using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models.Contract;
using Cirkla_DAL.Models.Items;
using Cirkla_API.Services;

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
        public async Task<ActionResult<Contract>> AskToBorrow(Contract contract)
        {
            if(contract is null)
            {
                return NotFound();
            }
            try
            {
                _borrowingService.AskForItem(contract);
                return Ok(contract);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
