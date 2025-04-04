using Cirkla_API.Common;
using Cirkla_API.Helpers;
using Cirkla_API.Services.Circles;
using Cirkla_API.Services.Users;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Mapping.DTOs.Circles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleController(ICircleService circleService,
                                    IUserService userService,
                                    ILogger<CircleController> logger) : ControllerBase
    {
        // GET: api/<CircleController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Circle>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Controller received request for all circles");
            var result = await circleService.GetAll();
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
            logger.LogInformation("Controller received request for circle with id: {Id}", id);
            var result = await circleService.GetById(id);
            return result.ToHttpResponse();
        }

        // POST api/<CircleController>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CircleCreateDTO circleDTO)
        {
            var result = await circleService.Create(circleDTO);
            return result.ToHttpResponse();
        }

        // PUT api/<CircleController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, Circle circle)
        {
            logger.LogInformation("Controller received request to update circle with id: {Id}", id);
            var result = await circleService.Update(id, circle);
            return result.ToHttpResponse();
        }

        // DELETE api/<CircleController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Controller received request to delete circle with id: {Id}", id);
            var result = await circleService.Delete(id);
            return result.ToHttpResponse();
        }
    }
}
