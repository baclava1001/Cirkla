using Cirkla_DAL.Models.Items;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        // TODO: Plugga på om ILogger och om jag ev behöver dependency injection
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemRepository itemRepository, ILogger<ItemController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        // TODO: Null-checks and other error-handling

        [HttpPost]
        public async Task<ActionResult<Item>> AddItemAsync(Item item)
        {
            if (item is null)
            {
                return BadRequest();
            }
            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveChangesAsync();
            // TODO: Return Created method instead
            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItemsAsync()
        {
            IEnumerable<Item> itemList = await _itemRepository.GetAllAsync();

            if (!itemList.Any())
            {
                return NotFound("No item found.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(itemList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemByIdAsync(int id)
        {
            Item item = await _itemRepository.GetByIdAsync(id);

            if (item is null)
            {
                return NotFound("No item found.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, Item item)
        {

            if (item is null || _itemRepository.GetByIdAsync(id) is null)
            {
                return NotFound("Can not update information.");
            }

            await _itemRepository.Update(item);
            await _itemRepository.SaveChangesAsync();
            Response.Headers.Append("Updated-Item-Id", item.Id.ToString());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAsync(int id)
        {
            Item item = await _itemRepository.GetByIdAsync(id);

            if (item is null)
            {
                return NotFound("Can not find item at this time.");
            }

            await _itemRepository.Remove(item);
            await _itemRepository.SaveChangesAsync();
            Response.Headers.Append("Removed-Item-Id", item.Id.ToString());
            return NoContent();
        }
    }
}
