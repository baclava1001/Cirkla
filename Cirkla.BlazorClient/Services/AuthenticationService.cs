using Blazored.LocalStorage;
using Cirkla.BlazorClient.Providers;
using Cirkla.ApiClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla.BlazorClient.Services
{
    /// <summary>
    /// This class is responsible for sending authentication requests and receiving and storing token.
    /// </summary>
    
    public class AuthenticationService(IClient client,
                                        ILocalStorageService localStorage,
                                        IApiAuthStateProvider apiAuthStateProvider) : IAuthenticationService
    {
        public async Task<bool> Authenticate(UserLoginDTO user)
        {
            try
            {
                var response = await client.LoginAsync(user);
                await localStorage.SetItemAsync("accessToken", response.Token);
                await apiAuthStateProvider.LoggedIn();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Logout()
        {
            await apiAuthStateProvider.LoggedOut();
        }
    }
}
