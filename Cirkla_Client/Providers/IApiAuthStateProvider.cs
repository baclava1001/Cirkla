using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_API.Providers
{
    public interface IApiAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task LoggedIn();
        Task LoggedOut();
    }
}