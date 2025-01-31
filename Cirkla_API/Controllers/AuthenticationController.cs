﻿using Microsoft.AspNetCore.Http;
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
        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup(UserSignupDTO userSignupDTO)
        {
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
                return Problem("Something went wrong while signing up.", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO)
        {
            // TODO: Extract logic to separate service
            try
            {
                User user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                bool passwordValid = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

                if (user == null || !passwordValid)
                {
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
                return Problem("Something went wrong while signing in.", statusCode: 500);
            }
        }


        // TODO: Move token logic to separate class
        private async Task<string> GenerateToken(User user)
        {
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
