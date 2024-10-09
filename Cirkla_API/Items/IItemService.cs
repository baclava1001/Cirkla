using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Items
{
    public interface IItemService
    {
        Task<bool> CreateItem(Item item);
        Task<IEnumerable<Item>> ListAllItems();
        Task<IEnumerable<Item>> ListAllItems(string ownerId);
        Task<Item> FindItemById(int id);
        Task<bool> DeleteItem(int id);
        Task<bool> UpdateItem(int id, Item item);
    }
}
