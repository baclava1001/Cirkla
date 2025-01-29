using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(string id);
        Task<User> Remove(User user);
        Task<User> Update(User user);
        Task SaveChanges();
    }
}
