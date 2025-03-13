using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleRequestController : ControllerBase
    {
        // POST-methods create new requests / invites:
        // UserRequestToJoin
        // UserRequestToBecomeAdmin
        // MemberInviteToUser
        // MembershipInviteFromAdmin
        // AdminInviteFromAdmin


        // GET-methods to get requests / invites:
        // Get all requests / invites for a circle (for admin)
        // Get all requests / invites for a user


        // PUT-methods update requests / invites:
        //Accepted member (=> adds member to circle)
        //Accepted admin (=> adds admin to circle)
        //Rejected
        //Revoked
        //Expired
    }
}
