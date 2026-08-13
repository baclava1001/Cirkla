using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Cirkla.API.Helpers;
using Cirkla.Data.Models;
using Cirkla.Shared.DTOs.Users;
using Cirkla.API.Common.Constants;
using Cirkla.API.Services.Authentication;
using Cirkla.API.Services.TokenGenerator;
using NSwag.Annotations;

namespace Cirkla.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthenticationController(ILogger<AuthenticationController> logger,
                                            ICustomAuthenticationService customAuthenticationService) : ControllerBase
    {
        [HttpPost]
        [Route("Signup")]
        [OpenApiOperation("Signup")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Signup(UserSignupDTO userSignupDTO)
        {
            logger.LogInformation("Signing up user");
            var result = await customAuthenticationService.Signup(userSignupDTO);
            return result.ToHttpResponse();
        }


        [HttpPost]
        [Route("Login")]
        [OpenApiOperation("Login")]
        [ProducesResponseType(typeof(UserAuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            logger.LogInformation("Logging in user with {Email}", userLoginDTO.Email);
            var result = await customAuthenticationService.Login(userLoginDTO);
            return result.ToHttpResponse();
        }
    }
}
