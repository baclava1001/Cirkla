using Cirkla_API.Services;
using Cirkla_DAL.Models.Items;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        // TODO: Plugga på om ILogger
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            if (item is null)
            {
                return BadRequest();
            }
            await _itemService.CreateItem(item);
            // TODO: Return Created method instead?
            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> ListAllItems(string ownerId)
        {
            IEnumerable<Item> items = await _itemService.ListAllItems(ownerId);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            Item item = await _itemService.FindItemById(id);

            if (item is null)
            {
                return NotFound("No item found.");
            }
            return Ok(item);
        }

        // TODO: Fix always returns success code even when operation not successful
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (await _itemService.UpdateItem(id, item) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Updated-Item-Id", item.Id.ToString());
            return NoContent();
        }

        // TODO: Fix always returns success code even when Item does not exist
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (await _itemService.DeleteItem(id) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Removed-Item-Id", id.ToString());
            return NoContent();
        }
    }
}
