using Cirkla_DAL.Models.Items;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Items
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
        // TODO: Remove async-suffix

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItemAsync(Item item)
        {
            if (item is null)
            {
                return BadRequest();
            }
            await _itemService.CreateItemAsync(item);
            // TODO: Return Created method instead?
            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> ListAllItemsAsync()
        {
            IEnumerable<Item> items = await _itemService.ListAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemByIdAsync(int id)
        {
            Item item = await _itemService.FindItemByIdAsync(id);

            if (item is null)
            {
                return NotFound("No item found.");
            }
            return Ok(item);
        }

        // TODO: Always returns success code even when operation not successful
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, Item item)
        {
            if(await _itemService.UpdateItem(id, item) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Updated-Item-Id", item.Id.ToString());
            return NoContent();
        }

        // TODO: Always returns success code even when Item does not exist
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(int id)
        {
            if(await _itemService.DeleteItem(id) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Removed-Item-Id", id.ToString());
            return NoContent();
        }
    }
}
