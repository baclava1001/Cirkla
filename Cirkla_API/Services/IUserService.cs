using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string id);
        Task<bool> Delete(string id);
        Task<User> Update(string id, User user);
    }
}
