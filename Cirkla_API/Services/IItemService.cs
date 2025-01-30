using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IItemService
    {
        Task<Item> Create(Item item);
        Task<IEnumerable<Item>> GetAll();
        Task<IEnumerable<Item>> GetAllItemsForUser(string ownerId);
        Task<Item> GetById(int id);
        Task<bool> Delete(int id);
        Task<Item> Update(int id, Item item);
    }
}
