using Cirkla_API.Repositories;
using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Services
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

        public async Task<bool> CreateItem(Item item)
        {
            if (item is null)
            {
                return false;
            }
            await _itemRepository.Add(item);
            await _itemRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteItem(int id)
        {
            Item item = await _itemRepository.GetItem(id);

            if (item is null)
            {
                return false;
                // return NotFound("Can not find item at this time.");
            }
            await _itemRepository.Remove(item);
            await _itemRepository.SaveChanges();
            return true;
        }

        // TODO: Tänk overloads!
        // Byt till "get"

        public async Task<Item> FindItemById(int id)
        {
            Item item = await _itemRepository.GetItem(id);
            return item;
        }

        public async Task<IEnumerable<Item>> ListAllItems()
        {
            IEnumerable<Item> itemList = await _itemRepository.GetAllItems();

            if (!itemList.Any())
            {
                // TODO: return error
            }
            return itemList;
        }

        public async Task<IEnumerable<Item>> ListAllItems(string userId)
        {
            IEnumerable<Item> itemList = await _itemRepository.GetAllItems(userId);

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
            await _itemRepository.SaveChanges();
            return true;
        }
    }
}
