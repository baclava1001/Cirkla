using Cirkla_API.ItemPictures;
using Cirkla_DAL.Models.ItemPictures;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Items
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPictureController : ControllerBase
    {
        private readonly IItemPictureRepository _itemPictureRepository;
        // TODO: Plugga på om ILogger och om jag ev behöver dependency injection
        private readonly ILogger<ItemPictureController> _logger;

        public ItemPictureController(IItemPictureRepository itemPictureRepository, ILogger<ItemPictureController> logger)
        {
            _itemPictureRepository = itemPictureRepository;
            _logger = logger;
        }

        // TODO: Null-checks and other error-handling

        [HttpPost]
        public async Task<ActionResult<ItemPicture>> AddItemPictureAsync(ItemPicture itemPicture)
        {
            if (itemPicture is null)
            {
                return BadRequest();
            }
            await _itemPictureRepository.AddAsync(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            // TODO: Return Created method instead
            return Ok(itemPicture);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPicture>>> GetAllItemsAsync()
        {
            IEnumerable<ItemPicture> itemPictureList = await _itemPictureRepository.GetAllAsync();

            if (!itemPictureList.Any())
            {
                return NotFound("No picture found.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(itemPictureList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPicture>> GetItemPictureByIdAsync(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetByIdAsync(id);

            if (itemPicture is null)
            {
                return NotFound("No picture found.");
            }
            // TODO: Ersätt med mappad DTO
            return Ok(itemPicture);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemPictureAsync(int id, ItemPicture itemPicture)
        {

            if (itemPicture is null || _itemPictureRepository.GetByIdAsync(id) is null)
            {
                return NotFound("Can not update information.");
            }

            await _itemPictureRepository.Update(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            Response.Headers.Append("Updated-ItemPicture-Id", itemPicture.Id.ToString());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPictureAsync(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetByIdAsync(id);

            if (itemPicture is null)
            {
                return NotFound("Can not find picture at this time.");
            }

            await _itemPictureRepository.Remove(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            Response.Headers.Append("Removed-ItemPicture-Id", itemPicture.Id.ToString());
            return NoContent();
        }
    }
}
