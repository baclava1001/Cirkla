using Cirkla_API.Helpers;
using Cirkla_API.Services.CircleMembership;
using Cirkla_DAL.Models;
using Cirkla.Shared.DTOs.CircleJoinRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleMembershipController(
        ICircleMembershipService circleMembershipService,
        ILogger<CircleMembershipController> logger)
        : ControllerBase
    {
        [HttpGet("/membership-requests/circles/{circleId}")]
        [OpenApiOperation("GetAllJoinRequestsForCircle")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForCircle(int circleId)
        {
            var result = await circleMembershipService.GetAllRequestsForCircle(circleId);
            return result.ToHttpResponse();
        }

        [HttpGet("/membership-requests/users/{userId}")]
        [OpenApiOperation("GetAllJoinRequestsForUser")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForUser(string userId)
        {
            var result = await circleMembershipService.GetAllRequestsForUser(userId);
            return result.ToHttpResponse();
        }


        [HttpGet("/membership-requests/users/{userId}/circle/{circleId}")]
        [OpenApiOperation("GetAllJoinRequestsForUserAndCircle")]
        [ProducesResponseType(typeof(IEnumerable<CircleJoinRequest>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRequestsForUser(string userId, int circleId)
        {
            var result = await circleMembershipService.GetAllRequestsForUserAndCircle(userId, circleId);
            return result.ToHttpResponse();
        }


        [HttpGet("/membership-requests/{id}")]
        [OpenApiOperation("GetJoinRequestById")]
        [ProducesResponseType(typeof(CircleJoinRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRequestById(int id)
        {
            var result = await circleMembershipService.GetRequestById(id);
            return result.ToHttpResponse();
        }


        [HttpPost("/membership-requests/")]
        [OpenApiOperation("SendJoinRequest")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendJoinRequest(CircleJoinRequestCreateDTO circleJoinRequestDTO)
        {
            var result = await circleMembershipService.SendJoinRequest(circleJoinRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/revoke")]
        [OpenApiOperation("RevokeJoinRequest")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await circleMembershipService.RevokeRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/reject")]
        [OpenApiOperation("RejectJoinRequest")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RejectRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await circleMembershipService.RejectRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("/membership-requests/{id}/Accept")]
        [OpenApiOperation("AcceptJoinRequest")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AcceptRequest(int id, CircleJoinRequestUpdateDTO circleRequestDTO)
        {
            var result = await circleMembershipService.AcceptRequest(id, circleRequestDTO);
            return result.ToHttpResponse();
        }



        [HttpPost("/membership-requests/users/{userId}/make-admin")]
        [OpenApiOperation("MakeAdmin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MakeAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
        {
            var result = await circleMembershipService.MakeAdmin(adminRequestDTO);
            return result.ToHttpResponse();
        }


        [HttpPost("/membership-requests/users/{userId}/remove-admin")]
        [OpenApiOperation("RemoveAdmin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveAdmin(CircleJoinRequestCreateDTO adminRequestDTO)
        {
            var result = await circleMembershipService.RemoveAdmin(adminRequestDTO);
            return result.ToHttpResponse();
        }


        // AdminRemoveMember
        // UserLeaveCircle
    }
}
