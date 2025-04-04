using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.ItemPictures;
using Cirkla_DAL.Repositories.UoW;

namespace Cirkla_API.Services.ItemPictures
{
    public class ItemPictureService(
        IItemPictureRepository itemPictureRepository,
        IUnitOfWork unitOfWork,
        ILogger<ItemPictureService> logger) : IItemPictureService
    {
        public async Task<ServiceResult<int>> Create(ItemPicture itemPicture)
        {
            if (itemPicture is null)
            {
                return ServiceResult<int>.Fail("Item picture is null", ErrorType.NotFound);
            }

            var createdItemPicture = await itemPictureRepository.Create(itemPicture);
            await unitOfWork.SaveChanges();
            return ServiceResult<int>.Success(createdItemPicture.Id);
        }


        public async Task<ServiceResult<object>> Update(int id, ItemPicture itemPicture)
        {
            if (itemPicture is null || id != itemPicture.Id)
            {
                logger.LogWarning("Attempted updating an item picture with null value or ID mismatch");
                return ServiceResult<object>.Fail("Item picture is not valid", ErrorType.ValidationError);
            }

            await itemPictureRepository.Update(itemPicture);
            await unitOfWork.SaveChanges();
            return ServiceResult<object>.Success(null);
        }


        public async Task<ServiceResult<object>> DeleteItemPicture(int id)
        {
            var itemPicture = await itemPictureRepository.GetById(id);

            if (itemPicture is null)
            {
                logger.LogWarning("Attempted to delete a non-existent item picture with ID {ItemId}", id);
                return ServiceResult<object>.Fail("Item picture not found", ErrorType.NotFound);
            }

            await itemPictureRepository.Delete(itemPicture);
            await unitOfWork.SaveChanges();
            return ServiceResult<object>.Success(null);
        }


        public async Task<ServiceResult<ItemPicture>> GetById(int id)
        {
            var itemPicture = await itemPictureRepository.GetById(id);

            if (itemPicture is null)
            {
                logger.LogWarning("Item picture with ID {ItemId} not found", id);
                return ServiceResult<ItemPicture>.Fail("Item picture not found", ErrorType.NotFound);
            }
            return ServiceResult<ItemPicture>.Success(itemPicture);
        }


        // Gets all images belonging to a specific item
        public async Task<ServiceResult<IEnumerable<ItemPicture>>> GetAllPicturesForItem(int itemId)
        {
            var itemPictureList = await itemPictureRepository.GetAll(itemId);

            if (!itemPictureList.Any())
            {
                logger.LogWarning("No item pictures found for item with ID {ItemId}", itemId);
                return ServiceResult<IEnumerable<ItemPicture>>.Fail("No item pictures found", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<ItemPicture>>.Success(itemPictureList);
        }
    }
}
