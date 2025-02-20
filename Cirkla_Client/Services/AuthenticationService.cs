using Blazored.LocalStorage;
using Cirkla_Client.Providers;
using Cirkla.ApiClient;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_Client.Services
{
    /// <summary>
    /// This class is responsible for sending authentication requests and recieving and storing token.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly IApiAuthStateProvider _apiAuthStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, IApiAuthStateProvider apiAuthStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _apiAuthStateProvider = apiAuthStateProvider;
        }


        public async Task<bool> Authenticate(UserLoginDTO user)
        {
            var response = await _client.ApiAuthenticationLoginAsync(user);
            await _localStorage.SetItemAsync("accessToken", response.Token);
            await _apiAuthStateProvider.LoggedIn();
            return true;
        }

        public async Task Logout()
        {
            await _apiAuthStateProvider.LoggedOut();
        }
    }
}
