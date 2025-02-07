using Cirkla_DAL.Models;

namespace Cirkla_API.Services.TokenGenerator;

public interface ITokenService
{
    Task<string> GenerateToken(User user);
}
