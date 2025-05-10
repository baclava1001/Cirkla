using Cirkla_API.Helpers;
using Cirkla_API.Services.Items;
using Cirkla_DAL.Models;
using Cirkla.Shared.DTOs.Items;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController(IItemService itemService, ILogger<ItemController> logger) : ControllerBase
    {

        // TODO: Add modelstate validation in all controller methods

        [HttpPost("items")]
        [OpenApiOperation("CreateItem")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(ItemCreateDTO itemDTO)
        {
            var result = await itemService.Create(itemDTO);
            return result.ToHttpResponse();
        }


        [HttpGet("items/users/{ownerId}")]
        [OpenApiOperation("GetAllItemsForUser")]
        [ProducesResponseType(typeof(IEnumerable<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllItemsForUser(string ownerId)
        {
            logger.LogInformation("Listing all items belonging to user with ID {ownerId}", ownerId);
            var result = await itemService.GetAllItemsForUser(ownerId);
            return result.ToHttpResponse();
        }


        [HttpGet("items")]
        [ProducesResponseType(typeof(IEnumerable<Item>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Listing all items");
            var result = await itemService.GetAll();
            return result.ToHttpResponse();
        }


        [HttpGet("items/{id}")]
        [OpenApiOperation("GetItemById")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            logger.LogInformation("Retrieving item with ID {ItemId}", id);
            var result = await itemService.GetById(id);
            return result.ToHttpResponse();
        }


        [HttpPut("{id}")]
        [OpenApiOperation("UpdateItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, Item item)
        {
            logger.LogInformation("Updating item with ID {ItemId}", id);
            var result = await itemService.Update(id, item);
            return result.ToHttpResponse();
        }


        [HttpDelete("{id}")]
        [OpenApiOperation("DeleteItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Deleting item with ID {ItemId}", id);
            var result = await itemService.Delete(id);
            return result.ToHttpResponse();
        }
    }
}
