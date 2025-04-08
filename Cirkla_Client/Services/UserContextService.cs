using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cirkla_Client.Services
{
    public class UserContext(AuthenticationStateProvider authProvider)
    {
        public async Task<string?> GetUserIdAsync()
        {
            var authState = await authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
