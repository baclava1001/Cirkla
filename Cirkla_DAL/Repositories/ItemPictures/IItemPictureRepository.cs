using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.ItemPictures
{
    public interface IItemPictureRepository
    {
        Task<ItemPicture> Create(ItemPicture itemPicture);
        Task<IEnumerable<ItemPicture>> GetAll(int itemId);
        Task<ItemPicture?> GetById(int id);
        Task<ItemPicture> Delete(ItemPicture itemPicture);
        Task<ItemPicture> Update(ItemPicture itemPicture);
        Task SaveChanges();
    }
}
