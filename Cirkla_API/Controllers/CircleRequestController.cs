using Cirkla_API.Helpers;
using Cirkla_API.Services.CircleRequests;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleRequestController : ControllerBase
    {
        private readonly ICircleRequestService _circleRequestService;
        private readonly ILogger<CircleRequestController> _logger;

        public CircleRequestController(ICircleRequestService circleRequestService,
            ILogger<CircleRequestController> logger)
        {
            _circleRequestService = circleRequestService;
            _logger = logger;
        }

        [HttpPost("UserRequestToJoin")]
        public async Task<IActionResult> UserRequestToJoin(CircleRequest circleRequest)
        {
            var result = await _circleRequestService.UserRequestsToJoin(circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPost("UserRequestToBecomeAdmin")]
        public async Task<IActionResult> UserRequestToBecomeAdmin(CircleRequest circleRequest)
        {
            var result = await _circleRequestService.UserRequestsToBecomeAdmin(circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPost("MemberInviteToUser")]
        public async Task<IActionResult> MemberInviteToUser(CircleRequest circleRequest)
        {
            var result = await _circleRequestService.MemberInvitesUser(circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPost("MembershipInviteFromAdmin")]
        public async Task<IActionResult> MembershipInviteFromAdmin(CircleRequest circleRequest)
        {
            var result = await _circleRequestService.MembershipInviteFromAdmin(circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPost("AdminInviteFromAdmin")]
        public async Task<IActionResult> AdminInviteFromAdmin(CircleRequest circleRequest)
        {
            var result = await _circleRequestService.AdminInviteFromAdmin(circleRequest);
            return result.ToHttpResponse();
        }

        [HttpGet("GetAllRequestsForCircle")]
        public async Task<IActionResult> GetAllRequestsForCircle(int circleId)
        {
            var result = await _circleRequestService.GetAllRequestsForCircle(circleId);
            return result.ToHttpResponse();
        }

        [HttpGet("GetAllRequestsForUser")]
        public async Task<IActionResult> GetAllRequestsForUser(string userId)
        {
            var result = await _circleRequestService.GetAllRequestsForUser(userId);
            return result.ToHttpResponse();
        }

        [HttpGet("GetRequestById")]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _circleRequestService.GetRequestById(id);
            return result.ToHttpResponse();
        }

        [HttpPut("RejectRequest")]
        public async Task<IActionResult> RejectRequest(int id, CircleRequest circleRequest)
        {
            var result = await _circleRequestService.RejectRequest(id, circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPut("RevokeRequest")]
        public async Task<IActionResult> RevokeRequest(int id, CircleRequest circleRequest)
        {
            var result = await _circleRequestService.RevokeRequest(id, circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPut("AdminAcceptsRequest")]
        public async Task<IActionResult> AdminAcceptsRequest(int id, CircleRequest circleRequest)
        {
            var result = await _circleRequestService.AdminAcceptsRequest(id, circleRequest);
            return result.ToHttpResponse();
        }

        [HttpPut("UserAcceptsInvite")]
        public async Task<IActionResult> UserAcceptsInvite(int id, CircleRequest circleRequest)
        {
            var result = await _circleRequestService.UserAcceptsInvite(id, circleRequest);
            return result.ToHttpResponse();
        }
    }
}
