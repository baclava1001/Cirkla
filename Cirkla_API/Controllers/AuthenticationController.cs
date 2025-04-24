using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Cirkla_API.Helpers;
using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.Mappers;
using Cirkla_API.Common.Constants;
using Cirkla_API.Services.Authentication;
using Cirkla_API.Services.TokenGenerator;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ILogger<AuthenticationController> logger,
                                            ICustomAuthenticationService customAuthenticationService) : ControllerBase
    {
        [HttpPost]
        [Route("Signup")]
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
