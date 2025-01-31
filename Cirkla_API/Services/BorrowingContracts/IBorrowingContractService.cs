using Cirkla_API.Common;
using Mapping.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.BorrowingContracts
{
    public interface IBorrowingContractService
    {
        Task<ServiceResult<Contract>> SendRequest(ContractCreateDTO contractCreateDTO);
        Task<ServiceResult<Contract>> ViewRequestSummary(int id);
        Task<ServiceResult<Contract>> RespondToRequest(int id, ContractReplyDTO contractReplyDTO);
    }
}
