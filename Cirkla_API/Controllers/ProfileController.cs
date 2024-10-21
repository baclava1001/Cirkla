using Cirkla_API.Services;
using Cirkla_DAL.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }


        // TODO: Remove this controller?
        [HttpPost]
        public async Task<IActionResult> CreateNewProfile(User user)
        {
            _logger.LogInformation("Adding user.");
            try
            {
                await _profileService.CreateProfile(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(string id, User user)
        {
            _logger.LogInformation("Updating profile by id.");
            if (user is null || id != user.Id)
            {
                _logger.LogError("User {id} does not exist.", id);
                return BadRequest("Can not update information.");
            }

            await _profileService.UpdateProfile(id, user);
            Response.Headers.Append("Updated-User-Id", user.Id.ToString());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
        {
            await _profileService.DeleteProfile(id);
            Response.Headers.Append("Removed-User-Id", id.ToString());
            return NoContent();
        }
    }
}
