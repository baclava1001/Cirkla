using Cirkla_DAL.Models.Contract;
using Cirkla_API.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Services
{
    public interface IBorrowingService
    {
        Task<Contract> AskForItem(ContractCreateDTO contractCreateDTO);
        Task<Contract> ViewRequestSummary(int id);
        Task<IEnumerable<Contract>> GetRequestsForInbox(string userId);
        Task<Contract> RespondToRequest(int id, ContractReplyDTO contractReplyDTO);
    }
}
