using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IItemPictureService
    {
        Task<bool> Create(ItemPicture itemPicture);
        Task<IEnumerable<ItemPicture>> GetAllPicturesForItem(int itemId);
        Task<ItemPicture> GetById(int id);
        Task<bool> DeleteItemPicture(int id);
        Task<bool> Update(int id, ItemPicture itemPicture);
    }
}