using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Users;

namespace Cirkla_API.Services.TimeLines;

public class LoanTrackingService(IContractRepository contractRepository,
                            ILogger<LoanTrackingService> logger) : ILoanTrackingService
{
    public async Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsBorrower(string userId)
    {
        var contracts = await contractRepository.GetActiveWhereUserIsBorrower(userId);
        if (!contracts.Any())
        {
            logger.LogWarning("No active contracts where user with id {Id} is borrower", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("No active contracts where user is borrower", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<Contract>>.Success(contracts);
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsOwner(string userId)
    {
        var contracts = await contractRepository.GetActiveWhereUserIsOwner(userId);
        if (!contracts.Any())
        {
            logger.LogWarning("No active contracts where user with id {Id} is owner", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("No active contracts where user is owner", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<Contract>>.Success(contracts);
    }

    public async Task<ServiceResult<IEnumerable<Contract>>> GetArchivedWhereUserWasOwner(string userId)
    {
        var contracts = await contractRepository.GetArchivedWhereUserWasOwner(userId);
        if (!contracts.Any())
        {
            logger.LogWarning("No archived contracts where user with id {Id} was owner", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("No archived contracts where user was owner", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<Contract>>.Success(contracts);
    }

    public async Task<ServiceResult<IEnumerable<Contract>>> GetArchivedWhereUsersWasBorrower(string userId)
    {
        var contracts = await contractRepository.GetArchivedWhereUsersWasBorrower(userId);
        if (!contracts.Any())
        {
            logger.LogWarning("No archived contracts where user with id {Id} was borrower", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("No archived contracts where user was borrower", ErrorType.NotFound);
        }
        return ServiceResult<IEnumerable<Contract>>.Success(contracts);
    }
}