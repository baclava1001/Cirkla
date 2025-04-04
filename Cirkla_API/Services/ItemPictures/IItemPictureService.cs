using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.ItemPictures
{
    public interface IItemPictureService
    {
        Task<ServiceResult<int>> Create(ItemPicture itemPicture);
        Task<ServiceResult<IEnumerable<ItemPicture>>> GetAllPicturesForItem(int itemId);
        Task<ServiceResult<ItemPicture>> GetById(int id);
        Task<ServiceResult<object>> DeleteItemPicture(int id);
        Task<ServiceResult<object>> Update(int id, ItemPicture itemPicture);
    }
}