using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IItemService
    {
        Task<ServiceResult<Item>> Create(Item item);
        Task<ServiceResult<IEnumerable<Item>>> GetAll();
        Task<ServiceResult<IEnumerable<Item>>> GetAllItemsForUser(string ownerId);
        Task<ServiceResult<Item>> GetById(int id);
        Task<ServiceResult<Item>> Delete(int id);
        Task<ServiceResult<Item>> Update(int id, Item item);
    }
}
