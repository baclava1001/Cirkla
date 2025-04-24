using Cirkla_API.Helpers;
using Cirkla_API.Services.CircleMembership;
using Cirkla_DAL.Models;
using Mapping.DTOs.CircleJoinRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleMembershipController : ControllerBase
    {
        private readonly ICircleMembershipService _circleMembershipService;
        private readonly ILogger<CircleMembershipController> _logger;

        public CircleMembershipController(ICircleMembershipService circleMembershipService,
            ILogger<CircleMembershipController> logger)
        {
            _circleMembershipService = circleMembershipService;
            _logger = logger;
        }


        [HttpGet("/membership-requests/circle/{circleId}")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForCircle(int circleId)
        {
            var result = await _circleMembershipService.GetAllRequestsForCircle(circleId);
            return result.ToHttpResponse();
        }

        [HttpGet("/membership-requests/user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForUser(string userId)
        {
            var result = await _circleMembershipService.GetAllRequestsForUser(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("/membership-requests/user/{userId}/circle/{circleId}")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForUser(string userId, int circleId)
        {
            var result = await _circleMembershipService.GetAllRequestsForUserAndCircle(userId, circleId);
            return result.ToHttpResponse();
        }


        [HttpGet("/membership-requests/{id}")]
        [ProducesResponseType(typeof(CircleJoinRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await _circleMembershipService.GetRequestById(id);
            return result.ToHttpResponse();
        }


        [HttpPost("/membership-requests/")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendJoinRequest(CircleJoinRequestCreateDTO circleJoinRequestDTO)
        {
            var result = await _circleMembershipService.SendJoinRequest(circleJoinRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/revoke")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await _circleMembershipService.RevokeRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/reject")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await _circleMembershipService.RejectRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/Accept")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AcceptRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await _circleMembershipService.AcceptRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }



        [HttpPost("/membership-requests/user{userId}/make-admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MakeAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
        {
            var result = await _circleMembershipService.MakeAdmin(adminRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPost("/membership-requests/user{userId}/remove-admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
        {
            var result = await _circleMembershipService.RemoveAdmin(adminRequestDTO);
            return result.ToHttpResponse();
        }


        // AdminRemoveMember
        // UserLeaveCircle
    }
}
