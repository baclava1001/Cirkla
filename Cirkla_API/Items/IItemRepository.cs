using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Items
{
    public interface IItemRepository
    {
        Task<Item> AddAsync(Item item);
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(int id);
        Task<Item> Remove(Item item);
        Task<Item> Update(Item item);
        Task SaveChangesAsync();
    }
}
