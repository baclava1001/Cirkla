using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.TokenGenerator;

public interface ITokenService
{
    Task<ServiceResult<string>> GenerateToken(User user);
}
