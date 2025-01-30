using Cirkla_API.Services;
using Cirkla_DAL.Models;
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
        public async Task<ActionResult<Item>> Create(Item item)
        {
            if (item is null)
            {
                return BadRequest();
            }
            await _itemService.Create(item);
            // TODO: Return CreatedAtAction method instead with 201 status code
            return Ok(item);
        }

        [HttpGet("ByUserId")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItemsForUser(string ownerId)
        {
            IEnumerable<Item> items = await _itemService.GetAllItemsForUser(ownerId);
            return Ok(items);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            IEnumerable<Item> items = await _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            Item item = await _itemService.GetById(id);

            if (item is null)
            {
                return NotFound("No item found.");
            }
            return Ok(item);
        }

        // TODO: Fix always returns success code even when operation not successful
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            try
            {
                await _itemService.Update(id, item);
                Response.Headers.Append("Updated-Item-Id", item.Id.ToString());
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // TODO: Fix always returns success code even when Item does not exist
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _itemService.Delete(id) == false)
            {
                return BadRequest();
            }
            Response.Headers.Append("Removed-Item-Id", id.ToString());
            return NoContent();
        }
    }
}
