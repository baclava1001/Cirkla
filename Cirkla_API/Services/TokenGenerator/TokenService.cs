using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Cirkla_API.Services.TokenGenerator;

public class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenService> _logger;

    public TokenService(UserManager<User> userManager, IConfiguration configuration, ILogger<TokenService> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ServiceResult<string>> GenerateToken(User user)
    {
        if (user == null)
        {
            _logger.LogWarning("Attempted to generate a token for a null user.");
            return ServiceResult<string>.Fail("User cannot be null", ErrorType.ValidationError);
        }

        try
        {
            _logger.LogInformation("Generating access token for user with {Email}", user.Email);

            var key = _configuration["JwtSettings:Key"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var durationString = _configuration["JwtSettings:DurationInMinutes"];

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience) || string.IsNullOrWhiteSpace(durationString))
            {
                _logger.LogError("JWT configuration settings are missing or invalid.");
                return ServiceResult<string>.Fail("Invalid JWT configuration", ErrorType.InternalError);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles?.Select(r => new Claim(ClaimTypes.Role, r)).ToList() ?? new List<Claim>();

            var userClaims = await _userManager.GetClaimsAsync(user) ?? new List<Claim>();

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
                _logger.LogError("Invalid JWT duration configuration.");
                return ServiceResult<string>.Fail("Invalid JWT duration configuration", ErrorType.InternalError);
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
            _logger.LogError(ex, "Error occurred while generating JWT token.");
            return ServiceResult<string>.Fail("An error occurred while generating the token", ErrorType.InternalError);
        }
    }

}