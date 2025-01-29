using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            _logger.LogInformation("Adding user.");
            if (user is null)
            {
                return BadRequest();
            }
            await _userRepository.Add(user);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            _logger.LogInformation("Listing all users.");
            IEnumerable<User> userList = await _userRepository.GetAll();

            if (!userList.Any())
            {
                return NotFound("No user registered.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(string id)
        {
            _logger.LogInformation("Retrieving user by id.");
            User user = await _userRepository.Get(id);

            if (user is null)
            {
                return NotFound("Can not find user at this time.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, User user)
        {
            _logger.LogInformation("Updating user info by id.");
            if (user is null || id != user.Id)
            {
                return BadRequest("Can not update information.");
            }

            await _userRepository.Update(user);
            await _userRepository.SaveChanges();
            Response.Headers.Append("Updated-User-Id", user.Id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("Deleting user by id.");
            User user = await _userRepository.Get(id);

            if (user is null)
            {
                return BadRequest("Can not find user at this time.");
            }

            await _userRepository.Remove(user);
            await _userRepository.SaveChanges();
            Response.Headers.Append("Removed-User-Id", user.Id);
            return NoContent();
        }
    }
}
