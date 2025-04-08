using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_Client.Providers
{
    /// <summary>
    /// This class contains logic for reading and setting authentication state.
    /// </summary>
    public class ApiAuthStateProvider(ILocalStorageService localStorage,
                                        JwtSecurityTokenHandler jwtSecurityTokenHandler) : AuthenticationStateProvider, IApiAuthStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await localStorage.GetItemAsync<string>("accessToken");

            if (savedToken is null)
            {
                return new AuthenticationState(user);
            }

            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            if (tokenContent.ValidTo < DateTime.Now)
            {
                return new AuthenticationState(user);
            }

            var claims = await GetClaims();
            user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            return new AuthenticationState(user);
        }


        public async Task LoggedIn()
        {
            var claims = await GetClaims();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }


        public async Task LoggedOut()
        {
            await localStorage.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }


        private async Task<IEnumerable<Claim>> GetClaims()
        {
            string? savedToken = await localStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
