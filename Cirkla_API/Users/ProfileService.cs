using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Users
{
    public class ProfileService : IProfileService
    {
        // TODO: Add error handling and logging (try global exception handling?)

        IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateProfileAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            await _userRepository.RemoveAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileAsync(int id, User user)
        {
            if (id == user.Id)
            {
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}
