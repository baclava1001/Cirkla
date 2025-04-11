using Blazored.LocalStorage;
using Cirkla_Client.Providers;
using Cirkla.ApiClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_Client.Services
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
                var response = await client.ApiAuthenticationLoginAsync(user);
                await localStorage.SetItemAsync("accessToken", response.Token);
                await apiAuthStateProvider.LoggedIn();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await apiAuthStateProvider.LoggedOut();
        }
    }
}
