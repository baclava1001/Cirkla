using Cirkla.API.Common;
using Cirkla.Data.Models;

namespace Cirkla.API.Services.Users
{
    public interface IUserService
    {
        Task<ServiceResult<string>> Create(User user);
        Task<ServiceResult<IEnumerable<User>>> GetAll();
        Task<ServiceResult<User>> GetById(string id);
        Task<ServiceResult<User>> Delete(string id);
        Task<ServiceResult<User>> Update(string id, User user);
    }
}
