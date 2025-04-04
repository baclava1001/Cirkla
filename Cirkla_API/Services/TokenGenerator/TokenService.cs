using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cirkla_API.Services.TokenGenerator;

public class TokenService(UserManager<User> userManager, 
                            IConfiguration configuration, 
                            ILogger<TokenService> logger) : ITokenService
{
    public async Task<ServiceResult<string>> GenerateToken(User user)
    {
        if (user == null)
        {
            logger.LogWarning("Attempted to generate a token for a null user.");
            return ServiceResult<string>.Fail("Authentication error", ErrorType.ValidationError);
        }

        try
        {
            logger.LogInformation("Generating access token for user with {Email}", user.Email);

            var key = configuration["JwtSettings:Key"];
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var durationString = configuration["JwtSettings:DurationInMinutes"];

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(durationString))
            {
                logger.LogError("JWT configuration settings are missing or invalid.");
                return ServiceResult<string>.Fail("Authentication error", ErrorType.InternalError);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles?.Select(r => new Claim(ClaimTypes.Role, r)).ToList() ?? new List<Claim>();

            var userClaims = await userManager.GetClaimsAsync(user) ?? new List<Claim>();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(CustomClaimTypes.UserId, user.Id ?? "")
            }
                .Union(roleClaims)
                .Union(userClaims);

            if (!int.TryParse(durationString, out var durationMinutes))
            {
                logger.LogError("Invalid JWT duration configuration.");
                return ServiceResult<string>.Fail("Authentication error", ErrorType.InternalError);
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationMinutes),
                signingCredentials: credentials
            );

            return ServiceResult<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while generating JWT token.");
            return ServiceResult<string>.Fail("Authentication error", ErrorType.InternalError);
        }
    }

}