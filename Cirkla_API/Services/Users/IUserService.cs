using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.Users
{
    public interface IUserService
    {
        Task<ServiceResult<string>> Create(User user);
        Task<ServiceResult<IEnumerable<User>>> GetAll();
        Task<ServiceResult<User>> GetById(string id);
        Task<ServiceResult<object>> Delete(string id);
        Task<ServiceResult<object>> Update(string id, User user);
    }
}
