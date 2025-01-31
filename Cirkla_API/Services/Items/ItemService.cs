using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Items;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }


        public async Task<ServiceResult<Item>> Create(Item item)
        {
            if (item is null)
            {
                _logger.LogWarning("Attempted creating an item with null value");
                return ServiceResult<Item>.Fail("Item is null", ErrorType.ValidationError);
            }

            try
            {
                var createdItem = await _itemRepository.Create(item);
                await _itemRepository.SaveChanges();
                return ServiceResult<Item>.Success(createdItem);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing new item to database");
                return ServiceResult<Item>.Fail("Error saving new item", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating new item");
                return ServiceResult<Item>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Item>> Delete(int id)
        {
            Item item = await _itemRepository.Get(id);
            if (item is null)
            {
                _logger.LogWarning("Attempted to delete a non-existent item with ID {ItemId}", id);
                return ServiceResult<Item>.Fail("Item not found", ErrorType.NotFound);
            }

            // TODO: Check if there are any active contracts before allowing deletion!

            try
            {
                await _itemRepository.Delete(item);
                await _itemRepository.SaveChanges();
                return ServiceResult<Item>.Success(item);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed deleting item with ID {ItemId} from database", id);
                return ServiceResult<Item>.Fail("Error deleting item", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting item with ID {ItemId}", id);
                return ServiceResult<Item>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Item>> GetById(int id)
        {
            try
            {
                var item = await _itemRepository.Get(id);
                if (item is null)
                {
                    _logger.LogWarning("Item with ID {ItemId} not found", id);
                    return ServiceResult<Item>.Fail("Item not found", ErrorType.NotFound);
                }
                return ServiceResult<Item>.Success(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting item with ID {ItemId}", id);
                return ServiceResult<Item>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<IEnumerable<Item>>> GetAll()
        {
            try
            {
                IEnumerable<Item> items = await _itemRepository.GetAll();
                if (!items.Any())
                {
                    _logger.LogWarning("No items found in database");
                    return ServiceResult<IEnumerable<Item>>.Fail("No items found", ErrorType.NotFound);
                }
                return ServiceResult<IEnumerable<Item>>.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting all items");
                return ServiceResult<IEnumerable<Item>>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<IEnumerable<Item>>> GetAllItemsForUser(string userId)
        {
            try
            {
                IEnumerable<Item> items = await _itemRepository.GetAllByOwnerId(userId);
                if (!items.Any())
                {
                    _logger.LogWarning("No items found in database for user with ID {UserId}", userId);
                    return ServiceResult<IEnumerable<Item>>.Fail("No items found", ErrorType.NotFound);
                }
                return ServiceResult<IEnumerable<Item>>.Success(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting all items for user with ID {UserId}", userId);
                return ServiceResult<IEnumerable<Item>>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Item>> Update(int id, Item item)
        {
            if (id != item.Id)
            {
                _logger.LogWarning("Item ID mismatch");
                return ServiceResult<Item>.Fail("Incorrect id", ErrorType.ValidationError);
            }

            try
            {
                var updatedItem = await _itemRepository.Update(item);
                await _itemRepository.SaveChanges();
                return ServiceResult<Item>.Success(item);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating item with ID {UserId}", id);
                return ServiceResult<Item>.Fail("Internal error, could not update item", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating item with ID {UserId}", id);
                return ServiceResult<Item>.Fail("Internal error", ErrorType.InternalError);
            }
        }
    }
}
