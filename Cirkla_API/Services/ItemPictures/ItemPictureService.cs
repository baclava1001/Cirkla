using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.ItemPictures;
using Cirkla_DAL.Repositories.UoW;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.ItemPictures
{
    public class ItemPictureService : IItemPictureService
    {
        private readonly IItemPictureRepository _itemPictureRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ItemPictureService> _logger;

        public ItemPictureService(IItemPictureRepository itemPictureRepository, IUnitOfWork unitOfWork, ILogger<ItemPictureService> logger)
        {
            _itemPictureRepository = itemPictureRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<ServiceResult<ItemPicture>> Create(ItemPicture itemPicture)
        {
            if (itemPicture is null)
            {
                return ServiceResult<ItemPicture>.Fail("Item picture is null", ErrorType.NotFound);
            }

            try
            {
                ItemPicture createdItemPicture = await _itemPictureRepository.Create(itemPicture);
                await _unitOfWork.SaveChanges();
                return ServiceResult<ItemPicture>.Success(createdItemPicture);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing new item picture to database");
                return ServiceResult<ItemPicture>.Fail("Error saving new item picture", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating new item picture");
                return ServiceResult<ItemPicture>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<ItemPicture>> Update(int id, ItemPicture itemPicture)
        {
            if (itemPicture is null || id != itemPicture.Id)
            {
                _logger.LogWarning("Attempted updating an item picture with null value or ID mismatch");
                return ServiceResult<ItemPicture>.Fail("Item picture is not valid", ErrorType.ValidationError);
            }

            try
            {
                ItemPicture updatedItemPicture = await _itemPictureRepository.Update(itemPicture);
                await _unitOfWork.SaveChanges();
                return ServiceResult<ItemPicture>.Success(updatedItemPicture);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing updated item picture with ID {Id} to database", id);
                return ServiceResult<ItemPicture>.Fail("Error saving updated item picture", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating item picture with ID {Id}", id);
                return ServiceResult<ItemPicture>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<ItemPicture>> DeleteItemPicture(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetById(id);

            if (itemPicture is null)
            {
                _logger.LogWarning("Attempted to delete a non-existent item picture with ID {ItemId}", id);
                return ServiceResult<ItemPicture>.Fail("Item picture not found", ErrorType.NotFound);
            }

            try
            {
                await _itemPictureRepository.Delete(itemPicture);
                await _unitOfWork.SaveChanges();
                return ServiceResult<ItemPicture>.Success(itemPicture);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed deleting item picture with ID {ItemId} from database", id);
                return ServiceResult<ItemPicture>.Fail("Error deleting item picture", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting item picture with ID {ItemId}", id);
                return ServiceResult<ItemPicture>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<ItemPicture>> GetById(int id)
        {
            try
            {
                ItemPicture itemPicture = await _itemPictureRepository.GetById(id);

                if (itemPicture is null)
                {
                    _logger.LogWarning("Item picture with ID {ItemId} not found", id);
                    return ServiceResult<ItemPicture>.Fail("Item picture not found", ErrorType.NotFound);
                }
                return ServiceResult<ItemPicture>.Success(itemPicture);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting item picture with ID {ItemId}", id);
                return ServiceResult<ItemPicture>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        // Gets all images belonging to a specific item
        public async Task<ServiceResult<IEnumerable<ItemPicture>>> GetAllPicturesForItem(int itemId)
        {
            try
            {
                IEnumerable<ItemPicture> itemPictureList = await _itemPictureRepository.GetAll(itemId);

                if (!itemPictureList.Any())
                {
                    _logger.LogWarning("No item pictures found for item with ID {ItemId}", itemId);
                    return ServiceResult<IEnumerable<ItemPicture>>.Fail("No item pictures found", ErrorType.NotFound);
                }
                return ServiceResult<IEnumerable<ItemPicture>>.Success(itemPictureList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting item pictures for item with ID {ItemId}", itemId);
                return ServiceResult<IEnumerable<ItemPicture>>.Fail("Internal server error", ErrorType.InternalError);
            }

        }
    }
}
