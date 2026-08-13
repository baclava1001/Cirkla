using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla.BlazorClient.Providers
{
    public interface IApiAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task LoggedIn();
        Task LoggedOut();
    }
}