using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.Mappers;
using Cirkla_API.Common.Constants;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;
        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }


        [HttpPost]
        [Route("Signup")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Signup(UserSignupDTO userSignupDTO)
        {
            _logger.LogInformation("Signing up user");
            // TODO: Move logic to a separate service
            try
            {
                User user = await Mapper.MapToUser(userSignupDTO);
                // Password is hashed and added to the user object below
                var result = await _userManager.CreateAsync(user, userSignupDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, ApiRoles.User);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while signing up user with {Email}", userSignupDTO.Email);
                return Problem("Something went wrong while signing up.", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(UserAuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO)
        {
            _logger.LogInformation("Logging in user with {Email}", userLoginDTO.Email);
            // TODO: Extract logic to separate service
            try
            {
                User user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                bool passwordValid = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

                if (user == null || !passwordValid)
                {
                    _logger.LogWarning("Failed login attempt for {Email}", userLoginDTO.Email);
                    return Unauthorized(userLoginDTO);
                }

                string tokenString = await GenerateToken(user);

                var response = new UserAuthResponseDTO
                {
                    Email = user.Email,
                    Token = tokenString,
                    Id = user.Id
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while signing in user with {Email}", userLoginDTO.Email);
                return Problem("Something went wrong while signing in.", statusCode: 500);
            }
        }


        // TODO: Move token logic to separate class
        private async Task<string> GenerateToken(User user)
        {
            _logger.LogInformation("Generating access token for user with {Email}", user.Email);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.UserId, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
