using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Items
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        // TODO: Exceptions, Return types that can return exceptions and objects, Logging

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<bool> CreateItemAsync(Item item)
        {
            if (item is null)
            {
                return false;
            }
            await _itemRepository.AddAsync(item);
            await _itemRepository.SaveChangesAsync();
            // TODO: Return Created method instead
            return true;
        }

        public async Task<bool> DeleteItem(int id)
        {
            Item item = await _itemRepository.GetByIdAsync(id);

            if (item is null)
            {
                return false;
                // return NotFound("Can not find item at this time.");
            }
            await _itemRepository.Remove(item);
            await _itemRepository.SaveChangesAsync();
            return true;
        }

        // TODO: Tänk overloads!
        // Byt till "get"

        public async Task<Item> FindItemByIdAsync(int id)
        {
            Item item = await _itemRepository.GetByIdAsync(id);
            return item;
        }

        public async Task<IEnumerable<Item>> ListAllItemsAsync()
        {
            IEnumerable<Item> itemList = await _itemRepository.GetAllAsync();

            if (!itemList.Any())
            {
                // TODO: return error
            }
            return itemList;
        }

        public async Task<bool> UpdateItem(int id, Item item)
        {
            if (item is null || item is null || id != item.Id)
            {
                return false;
            }
            await _itemRepository.Update(item);
            await _itemRepository.SaveChangesAsync();
            return true;
        }
    }
}
