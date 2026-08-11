using Cirkla.ApiClient;

namespace Cirkla.BlazorClient.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(UserLoginDTO user);
        Task Logout();
    }
}
