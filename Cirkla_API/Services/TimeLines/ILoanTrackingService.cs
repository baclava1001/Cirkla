using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.TimeLines;

public interface ILoanTrackingService
{
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsBorrower(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsOwner(string userId);
}