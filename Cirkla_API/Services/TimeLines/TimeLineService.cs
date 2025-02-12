using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Users;

namespace Cirkla_API.Services.TimeLines;

public class TimeLineService : ITimeLineService
{

    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<TimeLineService> _logger;

    public TimeLineService(IContractRepository contractRepository, IUserRepository userRepository, ILogger<TimeLineService> logger)
    {
        _contractRepository = contractRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsBorrower(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetActiveWhereUserIsBorrower(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No active contracts where user with id {Id} is borrower", userId);
                return ServiceResult<IEnumerable<Contract>>.Fail("No active contracts where user is borrower", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active contracts where user with id {Id} is borrower", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("Error retrieving contracts", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetActiveWhereUserIsOwner(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetActiveWhereUserIsOwner(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No active contracts where user with id {Id} is owner", userId);
                return ServiceResult<IEnumerable<Contract>>.Fail("No active contracts where user is owner", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active contracts where user with id {Id} is owner", userId);
            return ServiceResult<IEnumerable<Contract>>.Fail("Error retrieving contracts", ErrorType.InternalError);
        }
    }
}