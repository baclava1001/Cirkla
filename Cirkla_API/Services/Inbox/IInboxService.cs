using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.Inbox;

public interface IInboxService
{
    Task<ServiceResult<IEnumerable<Contract>>> GetIncomingRequestsForInbox(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetMyPendingRequests(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetMyAnsweredRequests(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetMyRequestHistory(string userId);
    Task<ServiceResult<IEnumerable<Contract>>> GetMyContractHistory(string userId);
}