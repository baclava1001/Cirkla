using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models.Contract;
using Cirkla_DAL.Models.Items;
using Cirkla_API.Services;
using Cirkla_API.DTOs;
using Cirkla_API.DTOs.Contracts;
using Cirkla_API.Helpers;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IMapper mapper, IBorrowingService borrowingService)
        {
            _mapper = mapper;
            _borrowingService = borrowingService;
        }

        // Ask to borrow = create contract for owner to review
        [HttpPost]
        public async Task<ActionResult<Contract>> AskToBorrow(ContractCreateDTO contractFromClient)
        {
            if(contractFromClient is null)
            {
                return NotFound();
            }
            Contract contract = await _mapper.MapContractCreateDtoToContract(contractFromClient);
            await _borrowingService.AskForItem(contract);
            return Ok(contract);
        }
    }
}
