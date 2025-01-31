using Cirkla_API.Helpers;
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
        public async Task<IActionResult> Create(Item item)
        {
            var result = await _itemService.Create(item);
            return result.ToHttpResponse();
        }


        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetAllItemsForUser(string ownerId)
        {
            _logger.LogInformation("Listing all items belonging to user with ID {ownerId}", ownerId);
            var result = await _itemService.GetAllItemsForUser(ownerId);
            return result.ToHttpResponse();
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Listing all items");
            var result = await _itemService.GetAll();
            return result.ToHttpResponse();
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Retrieving item with ID {ItemId}", id);
            var result = await _itemService.GetById(id);
            return result.ToHttpResponse();
        }


        // TODO: Fix always returns success code even when operation not successful
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            _logger.LogInformation("Updating item with ID {ItemId}", id);
            var result = await _itemService.Update(id, item);
            return result.ToHttpResponse();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting item with ID {ItemId}", id);
            var result = await _itemService.Delete(id);
            return result.ToHttpResponse();
        }
    }
}
