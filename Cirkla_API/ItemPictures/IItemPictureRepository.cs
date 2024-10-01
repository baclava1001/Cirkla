using Cirkla_DAL.Models.ItemPictures;

namespace Cirkla_API.ItemPictures
{
    public interface IItemPictureRepository
    {
        Task<ItemPicture> AddAsync(ItemPicture itemPicture);
        Task<IEnumerable<ItemPicture>> GetAllAsync();
        Task<ItemPicture> GetByIdAsync(int id);
        Task<ItemPicture> Remove(ItemPicture itemPicture);
        Task<ItemPicture> Update(ItemPicture itemPicture);
        Task SaveChangesAsync();
    }
}
