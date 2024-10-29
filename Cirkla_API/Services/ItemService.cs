using Cirkla_API.Repositories;
using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;

        // TODO: Exceptions and logging
        // TODO: Go through return types in methods below

        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        public async Task<Item> CreateItem(Item item)
        {
            try
            {
                await _itemRepository.Add(item);
                await _itemRepository.SaveChanges();
                return item;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        public async Task<Item> GetItem(int id)
        {
            Item item = await _itemRepository.GetItem(id);
            
            if(item is null)
            {
                return null;
            }
            return item;
        }

        public async Task<IEnumerable<Item>> ListAllItems()
        {
            IEnumerable<Item> items = await _itemRepository.GetAllItems();

            if (!items.Any())
            {
                return null;
            }
            return items;
        }

        public async Task<IEnumerable<Item>> ListAllItems(string userId)
        {
            IEnumerable<Item> items = await _itemRepository.GetAllItems(userId);

            if (!items.Any())
            {
                // TODO: return error
            }
            return items;
        }



        public async Task<Item> UpdateItem(int id, Item item)
        {
            if (item is null || id != item.Id)
            {
                return null;
            }
            try
            {
                await _itemRepository.Update(item);
                await _itemRepository.SaveChanges();
                return item;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return item;
        }
    }
}
