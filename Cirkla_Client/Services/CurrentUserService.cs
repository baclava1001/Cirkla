using Blazored.LocalStorage;
using Cirkla_Client.Extensions;
using Cirkla_Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Cirkla_Client.Services
{
    public class CurrentUserService(ILocalStorageService localStorage, JwtSecurityTokenHandler tokenHandler)
        : ApiAuthStateProvider(localStorage, tokenHandler)
    {
        public async Task<string> GetUserId()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                return user.GetUserId();
            }
            return string.Empty;
        }

        public async Task<string> GetUserName()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                return user.GetFullName();
            }
            return string.Empty;
        }

        public async Task<string> GetUserEmail()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                return user.GetEmail();
            }
            return string.Empty;
        }
    }
}
