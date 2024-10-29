using Cirkla_API.Services;
using Cirkla_DAL.Models.ItemPictures;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPictureController : ControllerBase
    {
        private readonly IItemPictureService _itemPictureService;
        // TODO: Plugga på om ILogger
        private readonly ILogger<ItemPictureController> _logger;

        public ItemPictureController(IItemPictureService itemPictureService, ILogger<ItemPictureController> logger)
        {
            _itemPictureService = itemPictureService;
            _logger = logger;
        }

        // TODO: Null-checks and other error-handling

        [HttpPost]
        public async Task<ActionResult<ItemPicture>> AddItemPictureAsync(ItemPicture itemPicture)
        {
            if (await _itemPictureService.AddItemPictureAsync(itemPicture) == false)
            {
                return BadRequest();
            }
            // TODO: Return Created method instead
            return Ok(itemPicture);
        }

        // Gets all images belonging to a specific item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPicture>>> ListItemPicturesAsync(int itemId)
        {
            IEnumerable<ItemPicture> itemPictures = await _itemPictureService.ListItemPicturesAsync(itemId);
            // TODO: Ersätt med mappad DTO
            return Ok(itemPictures);
        }

        // Gets a specific image
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPicture>> GetItemPictureByIdAsync(int id)
        {
            ItemPicture itemPicture = await _itemPictureService.FindItemPictureByIdAsync(id);
            // TODO: Ersätt med mappad DTO
            return Ok(itemPicture);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemPictureAsync(int id, ItemPicture itemPicture)
        {
            if (await _itemPictureService.ChangeItemPicture(id, itemPicture) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Updated-ItemPicture-Id", itemPicture.Id.ToString());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPictureAsync(int id)
        {
            if (await _itemPictureService.DeleteItemPicture(id) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Removed-ItemPicture-Id", id.ToString());
            return NoContent();
        }
    }
}
