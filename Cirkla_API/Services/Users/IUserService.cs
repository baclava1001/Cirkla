using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.Users
{
    public interface IUserService
    {
        Task<ServiceResult<User>> Create(User user);
        Task<ServiceResult<IEnumerable<User>>> GetAll();
        Task<ServiceResult<User>> GetById(string id);
        Task<ServiceResult<User>> Delete(string id);
        Task<ServiceResult<User>> Update(string id, User user);
    }
}
