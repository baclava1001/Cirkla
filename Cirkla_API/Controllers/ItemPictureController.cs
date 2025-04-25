using Cirkla_API.Helpers;
using Cirkla_API.Services.ItemPictures;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPictureController(IItemPictureService itemPictureService,
                                        ILogger<ItemPictureController> logger) : ControllerBase
    {
        [HttpPost("/items/create")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(ItemPicture itemPicture)
        {
            logger.LogInformation("Creating new item picture");
            var result = await itemPictureService.Create(itemPicture);
            return result.ToHttpResponse();
        }

        // Gets all images belonging to a specific item
        [HttpGet("/items/{itemId}/pictures/")]
        [ProducesResponseType(typeof(IEnumerable<ItemPicture>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPicturesForItem(int itemId)
        {
            logger.LogInformation("Getting all item pictures for item with ID {ItemId}", itemId);
            var result = await itemPictureService.GetAllPicturesForItem(itemId);
            return result.ToHttpResponse();
        }

        // Gets a specific image
        [HttpGet("/pictures/{id}")]
        [ProducesResponseType(typeof(ItemPicture), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            logger.LogInformation("Getting item picture with ID {Id}", id);
            var result = await itemPictureService.GetById(id);
            return result.ToHttpResponse();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, ItemPicture itemPicture)
        {
            logger.LogInformation("Updating item picture with ID {Id}", id);
            var result = await itemPictureService.Update(id, itemPicture);
            return result.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            logger.LogInformation("Deleting item picture with ID {Id}", id);
            var result = await itemPictureService.DeleteItemPicture(id);
            return result.ToHttpResponse();
        }
    }
}
