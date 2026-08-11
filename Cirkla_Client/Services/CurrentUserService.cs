using Blazored.LocalStorage;
using Cirkla.BlazorClient.Extensions;
using Cirkla.BlazorClient.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace Cirkla.BlazorClient.Services
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
