using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Services
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> ListAllUsers();
        Task<User> GetUser(string id);
        Task<bool> DeleteUser(string id);
        Task<User> UpdateUser(string id, User user);
    }
}
