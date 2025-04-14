using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Cirkla_Client.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }

    public static string? GetFullName(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
    }
}
