using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Cirkla.Shared.Mappers;
using Cirkla.Shared.DTOs.Items;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Items
{
    public class ItemService(
        IItemRepository itemRepository,
        IContractRepository contractRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<ItemService> logger) : IItemService
    {

        public async Task<ServiceResult<int>> Create(ItemCreateDTO itemDTO)
        {
            if (itemDTO is null)
            {
                logger.LogWarning("Attempted creating an item with null value");
                return ServiceResult<int>.Fail("Item could not be created", ErrorType.ValidationError);
            }
            var itemtoDb = await Mapper.MapToItem(itemDTO);
            await itemRepository.Create(itemtoDb);
            await unitOfWork.SaveChanges();
            return ServiceResult<int>.Created(itemtoDb.Id);
        }


        public async Task<ServiceResult<Item>> Delete(int id)
        {
            var item = await itemRepository.Get(id);
            if (item is null)
            {
                logger.LogWarning("Attempted to delete a non-existent item with ID {ItemId}", id);
                return ServiceResult<Item>.Fail("Item not found", ErrorType.NotFound);
            }

            var activeContracts = await contractRepository.GetActiveForItem(id);
            if (activeContracts.Any())
            {
                logger.LogWarning("Attempted to delete item with ID {ItemId} which still has active contracts", id);
                return ServiceResult<Item>.Fail("Item with active contracts cannot be deleted", ErrorType.ValidationError);
            }

            await itemRepository.Delete(item);
            await unitOfWork.SaveChanges();
            return ServiceResult<Item>.Success(item);
        }


        public async Task<ServiceResult<Item>> GetById(int id)
        {
            var item = await itemRepository.Get(id);
            if (item is null)
            {
                logger.LogWarning("Item with ID {ItemId} not found", id);
                return ServiceResult<Item>.Fail("Item not found", ErrorType.NotFound);
            }
            return ServiceResult<Item>.Success(item);
        }


        public async Task<ServiceResult<IEnumerable<Item>>> GetAll()
        {
            IEnumerable<Item> items = await itemRepository.GetAll();
            if (!items.Any())
            {
                logger.LogInformation("No items found in database");
                return ServiceResult<IEnumerable<Item>>.Fail("No items found", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Item>>.Success(items);
        }


        public async Task<ServiceResult<IEnumerable<Item>>> GetAllItemsForUser(string userId)
        {
            IEnumerable<Item> items = await itemRepository.GetAllByOwnerId(userId);
            if (!items.Any())
            {
                logger.LogInformation("No items found in database for user with ID {UserId}", userId);
                return ServiceResult<IEnumerable<Item>>.Fail("No items found", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Item>>.Success(items);
        }


        public async Task<ServiceResult<Item>> Update(int id, Item item)
        {
            // TODO: Add validation for availability - not possible to toggle "available" if there are active contracts and vice versa

            if (id != item.Id)
            {
                logger.LogWarning("Item ID mismatch");
                return ServiceResult<Item>.Fail("Incorrect id", ErrorType.ValidationError);
            }

            await itemRepository.Update(item);
            await unitOfWork.SaveChanges();
            return ServiceResult<Item>.Success(item);
        }
    }
}
