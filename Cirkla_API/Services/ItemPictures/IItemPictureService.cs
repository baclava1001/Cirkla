using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.ItemPictures
{
    public interface IItemPictureService
    {
        Task<ServiceResult<ItemPicture>> Create(ItemPicture itemPicture);
        Task<ServiceResult<IEnumerable<ItemPicture>>> GetAllPicturesForItem(int itemId);
        Task<ServiceResult<ItemPicture>> GetById(int id);
        Task<ServiceResult<ItemPicture>> DeleteItemPicture(int id);
        Task<ServiceResult<ItemPicture>> Update(int id, ItemPicture itemPicture);
    }
}