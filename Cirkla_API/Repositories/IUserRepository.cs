using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Repositories
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
