using Cirkla_DAL.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        // TODO: Plugga på om ILogger och om jag ev behöver dependency injection
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // TODO: Null-checks and other error-handling (try global exception handling?)

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User user)
        {
            if (user is null)
            {
                return BadRequest();
            }
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            IEnumerable<User> userList = await _userRepository.GetAllAsync();

            if (!userList.Any())
            {
                return NotFound("No user registered.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(userList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            
            if (user is null)
            {
                return NotFound("Can not find user at this time.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, User user)
        {
            if (user is null || id != user.Id)
            {
                return BadRequest("Can not update information.");
            }

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
            Response.Headers.Append("Updated-User-Id", user.Id.ToString());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                return BadRequest("Can not find user at this time.");
            }

            await _userRepository.RemoveAsync(user);
            await _userRepository.SaveChangesAsync();
            Response.Headers.Append("Removed-User-Id", user.Id.ToString());
            return NoContent();
        }
        


        // TODO: !!!"Fake login" (always returns true if user name is right)
    }
}
