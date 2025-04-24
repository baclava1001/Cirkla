using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Mapping.DTOs.Items;

namespace Cirkla_API.Services.Items
{
    public interface IItemService
    {
        Task<ServiceResult<int>> Create(ItemCreateDTO itemDTO);
        Task<ServiceResult<IEnumerable<Item>>> GetAll();
        Task<ServiceResult<IEnumerable<Item>>> GetAllItemsForUser(string ownerId);
        Task<ServiceResult<Item>> GetById(int id);
        Task<ServiceResult<Item>> Delete(int id);
        Task<ServiceResult<Item>> Update(int id, Item item);
    }
}
