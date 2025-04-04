using Cirkla_API.Helpers;
using Cirkla_API.Services.ItemPictures;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPictureController : ControllerBase
    {
        private readonly IItemPictureService _itemPictureService;
        private readonly ILogger<ItemPictureController> _logger;

        public ItemPictureController(IItemPictureService itemPictureService, ILogger<ItemPictureController> logger)
        {
            _itemPictureService = itemPictureService;
            _logger = logger;
        }


        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(ItemPicture itemPicture)
        {
            _logger.LogInformation("Creating new item picture");
            var result = await _itemPictureService.Create(itemPicture);
            return result.ToHttpResponse();
        }

        // Gets all images belonging to a specific item
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemPicture>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPicturesForItem(int itemId)
        {
            _logger.LogInformation("Getting all item pictures for item with ID {ItemId}", itemId);
            var result = await _itemPictureService.GetAllPicturesForItem(itemId);
            return result.ToHttpResponse();
        }

        // Gets a specific image
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ItemPicture), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Getting item picture with ID {Id}", id);
            var result = await _itemPictureService.GetById(id);
            return result.ToHttpResponse();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, ItemPicture itemPicture)
        {
            _logger.LogInformation("Updating item picture with ID {Id}", id);
            var result = await _itemPictureService.Update(id, itemPicture);
            return result.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting item picture with ID {Id}", id);
            var result = await _itemPictureService.DeleteItemPicture(id);
            return result.ToHttpResponse();
        }
    }
}
