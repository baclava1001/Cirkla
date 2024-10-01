using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Users
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> RemoveAsync(User user);
        Task<User> UpdateAsync(User user);
        Task SaveChangesAsync();
    }
}
