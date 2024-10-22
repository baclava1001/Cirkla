using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models.Contract;
using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        // Ask to borrow = create contract for owner to review
        [HttpPost]
        public async Task<ActionResult<Contract>> AskToBorrow(Contract contract)
        {
            return StatusCode(201, contract);
        }
    }
}
