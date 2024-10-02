using Cirkla_DAL.Models.Items;

namespace Cirkla_API.Items
{
    public interface IItemService
    {
        Task<bool> CreateItemAsync(Item item);
        Task<IEnumerable<Item>> ListAllItemsAsync();
        Task<Item> FindItemByIdAsync(int id);
        Task<bool> DeleteItem(int id);
        Task<bool> UpdateItem(int id, Item item);
    }
}
