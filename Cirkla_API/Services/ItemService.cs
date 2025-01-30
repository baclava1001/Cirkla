using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Items;

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

        public async Task<Item> Create(Item item)
        {
            try
            {
                await _itemRepository.Create(item);
                await _itemRepository.SaveChanges();
                return item;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> Delete(int id)
        {
            Item item = await _itemRepository.Get(id);

            if (item is null)
            {
                return false;
                // return NotFound("Can not find item at this time.");
            }
            await _itemRepository.Delete(item);
            await _itemRepository.SaveChanges();
            return true;
        }

        public async Task<Item> GetById(int id)
        {
            Item item = await _itemRepository.Get(id);
            
            if(item is null)
            {
                return null;
            }
            return item;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            IEnumerable<Item> items = await _itemRepository.GetAll();

            if (!items.Any())
            {
                return null;
            }
            return items;
        }

        public async Task<IEnumerable<Item>> GetAllItemsForUser(string userId)
        {
            IEnumerable<Item> items = await _itemRepository.GetAllByOwnerId(userId);

            if (!items.Any())
            {
                return null;
            }
            return items;
        }



        public async Task<Item> Update(int id, Item item)
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
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
