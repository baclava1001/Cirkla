using Cirkla_API.Common;
using Cirkla.Shared.DTOs.Contracts;
using Microsoft.AspNetCore.Mvc;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.BorrowingContracts
{
    public interface IBorrowingContractService
    {
        Task<ServiceResult<int>> SendRequest(ContractCreateDTO contractDTO);
        Task<ServiceResult<ContractResponseDTO>> ViewRequestSummary(int id);
        Task<ServiceResult<IEnumerable<ContractResponseDTO>>> GetActiveForItem(int itemId);
        Task<ServiceResult<ContractResponseDTO>> RespondToRequest(int id, ContractUpdateDTO contractUpdateDto);
        Task<ServiceResult<ContractResponseDTO>> CancelRequest(int id, ContractUpdateDTO contractUpdateDto);
        Task<ServiceResult<ContractResponseDTO>> ActivateRequest(int id, ContractUpdateDTO contractUpdateDto);
        Task<ServiceResult<ContractResponseDTO>> CompleteRequest(int id, ContractUpdateDTO contractUpdateDto);
    }
}
