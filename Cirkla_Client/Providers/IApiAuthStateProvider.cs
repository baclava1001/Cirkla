using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_Client.Providers
{
    public interface IApiAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task LoggedIn();
        Task LoggedOut();
    }
}