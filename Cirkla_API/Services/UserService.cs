using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Users;

namespace Cirkla_API.Services
{
    /// <summary>
    /// Simple CRUD-service for internal in other classes
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            try
            {
                await _userRepository.Add(user);
                await _userRepository.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            User user = new();

            try
            {
                user = await _userRepository.Get(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            try
            {
                await _userRepository.Remove(user);
                await _userRepository.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public async Task<User> GetUser(string id)
        {
            User user = new();

            try
            {
                user = await _userRepository.Get(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public async Task<IEnumerable<User>> ListAllUsers()
        {
            IEnumerable<User> users;

            try
            {
                users = await _userRepository.GetAll();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return users;
        }

        public async Task<User> UpdateUser(string id, User user)
        {
            if (user is null || id != user.Id)
            {
                return null;
            }
            try
            {
                await _userRepository.Update(user);
                await _userRepository.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
    }
}
