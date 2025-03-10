using Cirkla_API.Common;
using Cirkla_API.Helpers;
using Cirkla_API.Services.Circles;
using Cirkla_API.Services.Users;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleController : ControllerBase
    {
        private readonly ICircleService _circleService;
        private readonly IUserService _userService;
        private readonly ILogger<CircleController> _logger;

        public CircleController(ICircleService circleService, IUserService userService, ILogger<CircleController> logger)
        {
            _circleService = circleService;
            _userService = userService;
            _logger = logger;
        }

        // GET: api/<CircleController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Circle>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Controller received request for all circles");
            var result = await _circleService.GetAll();
            return result.ToHttpResponse();
        }

        // GET api/<CircleController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Circle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Controller received request for circle with id: {id}", id);
            var result = await _circleService.GetById(id);
            return result.ToHttpResponse();
        }

        // POST api/<CircleController>
        [HttpPost]
        [ProducesResponseType(typeof(Circle), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Circle circle)
        {
            _logger.LogInformation("Controller received request to create new circle");
            _logger.LogInformation("Getting user for populating circle");
            var userServiceResult = await _userService.GetById(circle.CreatedById);
            var user = userServiceResult.Payload;

            circle.CreatedBy = user;
            circle.Administrators = new List<User> { user };
            circle.Members = new List<User> { user };

            var result = await _circleService.Create(circle);
            return result.ToHttpResponse();
        }

        // PUT api/<CircleController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Circle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, Circle circle)
        {
            _logger.LogInformation("Controller received request to update circle with id: {id}", id);
            var result = await _circleService.Update(id, circle);
            return result.ToHttpResponse();
        }

        // DELETE api/<CircleController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Circle), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Controller received request to delete circle with id: {id}", id);
            var result = await _circleService.GetById(id);
            return result.ToHttpResponse();
        }
    }
}
