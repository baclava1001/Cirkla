using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(string id);
        Task<User> Delete(User user);
        Task<User> Update(User user);
    }
}
