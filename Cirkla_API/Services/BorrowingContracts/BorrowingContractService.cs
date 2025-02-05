using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Mapping.DTOs.Contracts;
using Mapping.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.BorrowingContracts
{
    public class BorrowingContractService : IBorrowingContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<BorrowingContractService> _logger;

        public BorrowingContractService(IContractRepository contractRepository, IItemRepository itemRepository, IUserRepository userRepository, ILogger<BorrowingContractService> logger)
        {
            _contractRepository = contractRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _logger = logger;
        }


        // Sends a request to borrow an item, by creating a new contract.
        public async Task<ServiceResult<Contract>> SendRequest(ContractCreateDTO contractDTOFromClient)
        {
            // TODO: Add validation check for Endtime > StartTime
            
            if (contractDTOFromClient is null)
            {
                _logger.LogWarning("Attempted creating a new borrowing contract with null value");
                return ServiceResult<Contract>.Fail("Request not valid", ErrorType.ValidationError);
            }

            var item = await _itemRepository.Get(contractDTOFromClient.ItemId);
            var owner = await _userRepository.Get(contractDTOFromClient.OwnerId);
            var borrower = await _userRepository.Get(contractDTOFromClient.BorrowerId);

            var contract = await Mapper.MapToContract(contractDTOFromClient, item, owner, borrower);

            if (contract.Item is null || contract.Owner is null || contract.Borrower is null)
            {
                _logger.LogWarning("Invalid contract details");
                return ServiceResult<Contract>.Fail("Invalid contract details", ErrorType.NotFound);
            }

            try
            {
                await _contractRepository.Create(contract);
                await _contractRepository.SaveChanges();
                return ServiceResult<Contract>.Success(contract);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing new contract to database");
                return ServiceResult<Contract>.Fail("Error saving new contract", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating new contract");
                return ServiceResult<Contract>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Contract>> ViewRequestSummary(int id)
        {
            try
            {
                var contract = await _contractRepository.GetById(id);
                if (contract is null)
                {
                    _logger.LogWarning("Borrowing contract with ID {id} not found", id);
                    return ServiceResult<Contract>.Fail("Borrowing contract not found", ErrorType.NotFound);
                }

                return ServiceResult<Contract>.Success(contract);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting borrowing contract with ID {id}", id);
                return ServiceResult<Contract>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Contract>> RespondToRequest(int id, ContractReplyDTO contractReplyDTO)
        {
            if (contractReplyDTO is null || id != contractReplyDTO.Id)
            {
                _logger.LogWarning("Attempted updating an borrowing contract (to reply to a request) with null value or ID mismatch");
                return ServiceResult<Contract>.Fail("Reply not valid", ErrorType.ValidationError);
            }

            Contract contract = await Mapper.MapToContract(contractReplyDTO);
            contract.Item = await _itemRepository.Get(contractReplyDTO.ItemId);
            contract.Owner = await _userRepository.Get(contractReplyDTO.OwnerId);
            contract.Borrower = await _userRepository.Get(contractReplyDTO.BorrowerId);

            try
            {
                await _contractRepository.Update(contract);
                await _contractRepository.SaveChanges();
                return ServiceResult<Contract>.Success(contract);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Failed writing updated borrowing contract with ID {Id}", id);
                return ServiceResult<Contract>.Fail("Error saving updated borrowing contract", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating borrowing contract with ID {Id}", id);
                return ServiceResult<Contract>.Fail("Internal server error", ErrorType.InternalError);
            }
        }
    }
}
