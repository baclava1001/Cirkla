using Cirkla.ApiClient;

namespace Cirkla_Client.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(UserLoginDTO user);
        Task Logout();
    }
}
