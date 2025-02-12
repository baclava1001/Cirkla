using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.TimeLines;

public interface ITimeLineService
{
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsBorrower(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsOwner(string userId);
}