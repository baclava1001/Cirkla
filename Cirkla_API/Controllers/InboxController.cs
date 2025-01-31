using Cirkla_API.Services;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    ///<summary>Gets the requests and contracts for the user's inbox.</summary>
    public class InboxController : ControllerBase
    {
        private readonly IBorrowingContractService _borrowingContractService;

        public InboxController(IBorrowingContractService borrowingContractService)
        {
            _borrowingContractService = borrowingContractService;
        }

        // TODO: Return thin DTO:s with bare minimum - each request/contract will be accessible from their details page (ViewRequestSummary)
        [HttpGet("RequestsToInbox")]
        public async Task<ActionResult<IEnumerable<Contract>>> IncomingRequestsToInbox(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingContractService.GetIncomingRequestsForInbox(userId);
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
                contracts = await _borrowingContractService.GetMyPendingRequests(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }


        [HttpGet("MyAnsweredRequests")]
        public async Task<ActionResult<IEnumerable<Contract>>> MyAnsweredRequests(string userId)
        {
            IEnumerable<Contract> contracts = null;
            try
            {
                contracts = await _borrowingContractService.GetMyAnsweredRequests(userId);
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
                contracts = await _borrowingContractService.GetMyRequestHistory(userId);
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
                contracts = await _borrowingContractService.GetMyContractHistory(userId);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return Ok(contracts);
        }
    }
}
