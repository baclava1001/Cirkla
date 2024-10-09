using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Items
{
    public interface IItemRepository
    {
        Task<Item> Add(Item item);
        Task<IEnumerable<Item>> GetAllItems();
        Task<IEnumerable<Item>> GetAllItems(string userId);
        Task<Item> GetItem(int id);
        Task<Item> Remove(Item item);
        Task<Item> Update(Item item);
        Task SaveChangesAsync();
    }
}
