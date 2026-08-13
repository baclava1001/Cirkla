using Cirkla.API.Common;
using Cirkla.Data.Models;

namespace Cirkla.API.Services.TimeLines;

public interface ILoanTrackingService
{
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsBorrower(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsOwner(string userId);
}