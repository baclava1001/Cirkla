using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.ItemPictures
{
    public interface IItemPictureRepository
    {
        Task<ItemPicture> AddAsync(ItemPicture itemPicture);
        Task<IEnumerable<ItemPicture>> GetAllAsync(int itemId);
        Task<ItemPicture> GetByIdAsync(int id);
        Task<ItemPicture> Remove(ItemPicture itemPicture);
        Task<ItemPicture> Update(ItemPicture itemPicture);
        Task SaveChangesAsync();
    }
}
