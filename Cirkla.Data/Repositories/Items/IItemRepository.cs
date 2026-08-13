using Cirkla.Data.Models;

namespace Cirkla.Data.Repositories.Items
{
    public interface IItemRepository
    {
        Task<Item> Create(Item item);
        Task<IEnumerable<Item>> GetAll();
        Task<IEnumerable<Item>> GetAllByOwnerId(string userId);
        Task<Item?> Get(int id);
        Task<Item> Delete(Item item);
        Task<Item> Update(Item item);
    }
}
