﻿using Cirkla_API.Helpers;
using Cirkla_API.Services.BorrowingContracts;
using Cirkla_API.Services.Inbox;
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
        private readonly IInboxService _inboxService;
        private readonly ILogger _logger;

        public InboxController(IInboxService inboxService, ILogger logger)
        {
            _inboxService = inboxService;
            _logger = logger;
        }


        // TODO: Return thin DTO:s with bare minimum - each request/contract will be accessible from their details page (ViewRequestSummary)
        [HttpGet("RequestsToInbox")]
        public async Task<IActionResult> IncomingRequestsToInbox(string userId)
        {
            _logger.LogInformation("Getting requests to user for inbox");
            var result = await _inboxService.GetIncomingRequestsForInbox(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("MyPendingRequests")]
        public async Task<IActionResult> MyPendingRequests(string userId)
        {
            _logger.LogInformation("Getting user's pending requests");
            var result = await _inboxService.GetIncomingRequestsForInbox(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("MyAnsweredRequests")]
        public async Task<IActionResult> MyAnsweredRequests(string userId)
        {
            _logger.LogInformation("Getting user's answered requests");
            var result = await _inboxService.GetIncomingRequestsForInbox(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("MyRequestHistory")]
        public async Task<IActionResult> MyRequestHistory(string userId)
        {
            _logger.LogInformation("Getting user's request history");
            var result = await _inboxService.GetIncomingRequestsForInbox(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("MyContractHistory")]
        public async Task<IActionResult> MyContractHistory(string userId)
        {
            _logger.LogInformation("Getting user's finalizied borrowing contract history");
            var result = await _inboxService.GetIncomingRequestsForInbox(userId);
            return result.ToHttpResponse();
        }
    }
}
