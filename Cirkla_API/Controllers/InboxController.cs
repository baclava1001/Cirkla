using Cirkla_API.Services;
using Cirkla_DAL.Models.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InboxController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;

        public InboxController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        // TODO: Return thin DTO:s with bare minimum - each request/contract will be accessible from their details page (ViewRequestSummary)
        [HttpGet("RequestsToInbox")]
        public async Task<ActionResult<IEnumerable<Contract>>> IncomingRequestsToInbox(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingService.GetIncomingRequestsForInbox(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }


        [HttpGet("MyPendingRequests")]
        public async Task<ActionResult<IEnumerable<Contract>>> MyPendingRequests(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingService.GetMyPendingRequests(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }


        [HttpGet("MyRequestHistory")]
        public async Task<ActionResult<IEnumerable<Contract>>> MyRequestHistory(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingService.GetMyRequestHistory(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }


        [HttpGet("MyContractHistory")]
        public async Task<ActionResult<IEnumerable<Contract>>> MyContractHistory(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingService.GetMyContractHistory(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }
    }
}
