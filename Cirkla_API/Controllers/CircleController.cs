using Cirkla_API.Common;
using Cirkla_API.Helpers;
using Cirkla_API.Services.Circles;
using Cirkla_API.Services.Users;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Cirkla.Shared.DTOs.Circles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircleController(ICircleService circleService,
                                    IUserService userService,
                                    ILogger<CircleController> logger) : ControllerBase
    {
        [HttpGet("circles")]
        [OpenApiOperation("GetAllCircles")]
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


        [HttpGet("circles/{id}")]
        [OpenApiOperation("GetCircleById")]
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


        [HttpPost("circles")]
        [OpenApiOperation("CreateCircle")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CircleCreateDTO circleDTO)
        {
            var result = await circleService.Create(circleDTO);
            return result.ToHttpResponse();
        }


        [HttpPut("circles/{id}")]
        [OpenApiOperation("UpdateCircle")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, Circle circle)
        {
            logger.LogInformation("Controller received request to update circle with id: {Id}", id);
            var result = await circleService.Update(id, circle);
            return result.ToHttpResponse();
        }


        [HttpDelete("circles/{id}")]
        [OpenApiOperation("DeleteCircle")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
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
