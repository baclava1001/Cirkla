using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Users;

namespace Cirkla_API.Services.Inbox;

public class InboxService : IInboxService
{
    private readonly IContractRepository _contractRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public InboxService(IContractRepository contractRepository, IUserRepository userRepository, ILogger<InboxService> logger)
    {
        _contractRepository = contractRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ServiceResult<IEnumerable<Contract>>> GetIncomingRequestsForInbox(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetIncomingRequestsForInbox(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No incoming requests for inbox");
                return ServiceResult<IEnumerable<Contract>>.Fail("No incoming requests for inbox", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting incoming requests for inbox");
            return ServiceResult<IEnumerable<Contract>>.Fail("Error getting incoming requests for inbox", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetMyPendingRequests(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetUsersPendingRequests(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No pending requests");
                return ServiceResult<IEnumerable<Contract>>.Fail("No pending requests", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting pending requests");
            return ServiceResult<IEnumerable<Contract>>.Fail("Error getting pending requests", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetMyAnsweredRequests(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetUsersAnsweredRequests(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No answered requests");
                return ServiceResult<IEnumerable<Contract>>.Fail("No answered requests", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting pending requests");
            return ServiceResult<IEnumerable<Contract>>.Fail("Error getting pending requests", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetMyRequestHistory(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetUsersRequestHistory(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No requests in history");
                return ServiceResult<IEnumerable<Contract>>.Fail("No requests in history", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting historical requests");
            return ServiceResult<IEnumerable<Contract>>.Fail("Error getting old requests", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<IEnumerable<Contract>>> GetMyContractHistory(string userId)
    {
        try
        {
            var contracts = await _contractRepository.GetUsersContractHistory(userId);
            if (!contracts.Any())
            {
                _logger.LogWarning("No contracts in history");
                return ServiceResult<IEnumerable<Contract>>.Fail("No contracts in history", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Contract>>.Success(contracts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting historical contracts");
            return ServiceResult<IEnumerable<Contract>>.Fail("Error getting old contracts", ErrorType.InternalError);
        }
    }
}