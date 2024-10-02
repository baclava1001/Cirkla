using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Users
{
    public interface IProfileService
    {
        Task<bool> CreateProfileAsync(User user);
        Task<bool> DeleteProfileAsync(int id);
        Task<bool> UpdateProfileAsync(int id, User user);
        // TODO: DeactivateProfile?
    }
}
