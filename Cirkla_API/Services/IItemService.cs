using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IItemService
    {
        Task<Item> CreateItem(Item item);
        Task<IEnumerable<Item>> ListAllItems();
        Task<IEnumerable<Item>> ListAllItems(string ownerId);
        Task<Item> GetItem(int id);
        Task<bool> DeleteItem(int id);
        Task<Item> UpdateItem(int id, Item item);
    }
}
