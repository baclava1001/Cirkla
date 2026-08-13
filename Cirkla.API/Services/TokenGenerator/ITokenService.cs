using Cirkla.API.Common;
using Cirkla.Data.Models;

namespace Cirkla.API.Services.TokenGenerator;

public interface ITokenService
{
    Task<ServiceResult<string>> GenerateToken(User user);
}
