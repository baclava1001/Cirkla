using Mapping.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services
{
    public interface IBorrowingService
    {
        Task<Contract> AskForItem(ContractCreateDTO contractCreateDTO);
        Task<Contract> ViewRequestSummary(int id);
        Task<IEnumerable<Contract>> GetIncomingRequestsForInbox(string userId);
        Task<IEnumerable<Contract>> GetMyPendingRequests(string userId);
        Task<IEnumerable<Contract>> GetMyAnsweredRequests(string userId);
        Task<IEnumerable<Contract>> GetMyRequestHistory(string userId);
        Task<IEnumerable<Contract>> GetMyContractHistory(string userId);

        Task<Contract> RespondToRequest(int id, ContractReplyDTO contractReplyDTO);
    }
}
