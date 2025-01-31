using Cirkla_API.Helpers;
using Cirkla_API.Services.Users;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _logger.LogInformation("Adding user.");
            var result = await _userService.Create(user);
            return result.ToHttpResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listing all users.");
            var result = await _userService.GetAll();
            return result.ToHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            _logger.LogInformation("Retrieving user by id.");
            var result = await _userService.GetById(id);
            return result.ToHttpResponse();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            _logger.LogInformation("Updating user info by id.");
            var result = await _userService.Update(id, user);
            return result.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting user by id.");
            var result = await _userService.Delete(id);
            return result.ToHttpResponse();
        }
    }
}
