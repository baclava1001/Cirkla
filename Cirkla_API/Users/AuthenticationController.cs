using Cirkla_DAL.Models.Users;
using Cirkla_DAL.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public AuthenticationController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        // TODO: Scrap either this method or the ProfileController CreateNewProfile method
        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Register(UserPostDTOs userPostDTO)
        {
            // TODO: Move logic to a separate service
            try
            {
                // TODO: Add Automapper at a later stage
                User user = new()
                {
                    UserName = userPostDTO.Email,
                    Email = userPostDTO.Email,
                    FirstName = userPostDTO.FirstName,
                    LastName = userPostDTO.LastName,
                    Address = userPostDTO.Address,
                    ZipCode = userPostDTO.ZipCode,
                    ProfilePictureURL = userPostDTO.ProfilePictureURL,
                    EmailConfirmed = true
                    // Password is hashed and added to the user object below
                };
                var result = await _userManager.CreateAsync(user, userPostDTO.Password);

                if(!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRoleAsync(user, ApiRole.User);
                return Accepted(user);
            }
            catch(Exception ex)
            {
                return Problem("Something went wrong while signing up.", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                User user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
                bool passwordValid = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

                if(user == null || !passwordValid)
                {
                    return NotFound();
                }

                // TODO: Add JWT-logic here

                return Accepted();
            }
            catch(Exception ex)
            {
                return Problem("Something went wrong while signing in.", statusCode: 500);
            }
        }

    }
}
