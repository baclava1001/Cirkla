using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Services
{
    public interface IProfileService
    {
        Task<bool> CreateProfile(User user);
        Task<bool> DeleteProfile(string id);
        Task<bool> UpdateProfile(string id, User user);
        // TODO: DeactivateProfile?
    }
}
