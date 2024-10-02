using Cirkla_DAL.Models.ItemPictures;

namespace Cirkla_API.ItemPictures
{
    public interface IItemPictureService
    {
        Task<bool> AddItemPictureAsync(ItemPicture itemPicture);
        Task<IEnumerable<ItemPicture>> ListItemPicturesAsync(int itemId);
        Task<ItemPicture> FindItemPictureByIdAsync(int id);
        Task<bool> DeleteItemPicture(int id);
        Task<bool> ChangeItemPicture(int id, ItemPicture itemPicture);
    }
}