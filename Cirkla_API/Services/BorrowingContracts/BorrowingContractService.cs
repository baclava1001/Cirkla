﻿using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Services.ContractNotifications;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Cirkla.Shared.Mappers;
using FluentValidation;
using Cirkla.Shared.DTOs.Contracts;

namespace Cirkla_API.Services.BorrowingContracts
{
    public class BorrowingContractService(
        IContractRepository contractRepository,
        IItemRepository itemRepository,
        IUserRepository userRepository,
        IContractNotificationService contractNotificationService,
        IUnitOfWork unitOfWork,
        IValidator<ContractCreateDTO> contractCreateValidator,
        IValidator<ContractUpdateDTO> contractUpdateValidator,
        ILogger<BorrowingContractService> logger) : IBorrowingContractService
    
    {

        #region Creating

        // Sends a request to borrow an item, by creating a new contract.
        public async Task<ServiceResult<int>> SendRequest(ContractCreateDTO contractDTO)
        {
            var validationResult = await contractCreateValidator.ValidateAsync(contractDTO);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                logger.LogWarning("Contract create validation failed: {Errors}", errorMessages);
                return ServiceResult<int>.Fail(errorMessages, ErrorType.ValidationError);
            }

            var contract = await Mapper.MapToContract(contractDTO);

            //// Delete? Added a better fluent validation to check if the item is already borrowed - at an overlapping timeframe
            //if (await IsAlreadyBorrowed(contract.ItemId))
            //{
            //    logger.LogWarning("Item with ID {ItemId} is already borrowed", contract.ItemId);
            //    return ServiceResult<int>.Fail("Item is already borrowed", ErrorType.ValidationError);
            //}

            await UpdateContractStatus(contract, ContractStatus.Pending, contract.BorrowerId);
            await contractRepository.Create(contract);
            await unitOfWork.SaveChangesWithTransaction();

            contract = await contractRepository.GetById(contract.Id);
            _ = contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients

            return ServiceResult<int>.Created(contract.Id);
        }

        #endregion

        #region Getting

        public async Task<ServiceResult<ContractResponseDTO>> ViewRequestSummary(int id)
        {
            var contract = await contractRepository.GetById(id);
            if (contract is null)
            {
                logger.LogWarning("Borrowing contract with ID {Id} not found", id);
                return ServiceResult<ContractResponseDTO>.Fail("Borrowing contract not found", ErrorType.NotFound);
            }
            var contractResponse = await Mapper.MapToContractResponseDTO(contract);
            return ServiceResult<ContractResponseDTO>.Success(contractResponse);
        }

        public async Task<ServiceResult<IEnumerable<ContractResponseDTO>>> GetActiveForItem(int itemId)
        {
            var contracts = await contractRepository.GetActiveForItem(itemId);
            if (contracts is null)
            {
                logger.LogWarning("No active borrowing contracts found for item with id {Id}", itemId);
                return ServiceResult<IEnumerable<ContractResponseDTO>>.Fail("No borrowing contracts found", ErrorType.NotFound);
            }

            var contractResponses = new List<ContractResponseDTO>();
            foreach (var contract in contracts)
            {
                var contactDTO = await Mapper.MapToContractResponseDTO(contract);
                contractResponses.Add(contactDTO);
            }
            return ServiceResult<IEnumerable<ContractResponseDTO>>.Success(contractResponses);
        }

        #endregion

        #region Updating

        // Updates a borrowing contract to respond to a request.
        public async Task<ServiceResult<ContractResponseDTO>> RespondToRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            var validationResult = await contractUpdateValidator.ValidateAsync(contractUpdateDto);
            if(!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                logger.LogError("Contract update validation failed: {Errors}", errorMessages);
                return ServiceResult<ContractResponseDTO>.Fail(errorMessages, ErrorType.ValidationError);
            }

            var contract = await contractRepository.GetById(id);
            if (contract == null)
            {
                logger.LogWarning("Contract with ID {ContractId} not found", id);
                return ServiceResult<ContractResponseDTO>.Fail("Contract not found", ErrorType.NotFound);
            }

            UpdateContractAndStatus(contract, contractUpdateDto);
            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();
            
            _ = contractNotificationService
                    .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            
            var contractResponse = await Mapper.MapToContractResponseDTO(contract);
            return ServiceResult<ContractResponseDTO>.Success(contractResponse);
        }


        public async Task<ServiceResult<ContractResponseDTO>> CancelRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            var validationResult = await contractUpdateValidator.ValidateAsync(contractUpdateDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                logger.LogError("Contract update validation failed: {Errors}", errorMessages);
                return ServiceResult<ContractResponseDTO>.Fail(errorMessages, ErrorType.ValidationError);
            }

            var contract = await contractRepository.GetById(id);
            if (contract == null)
            {
                logger.LogWarning("Contract with ID {ContractId} not found", id);
                return ServiceResult<ContractResponseDTO>.Fail("Contract not found", ErrorType.NotFound);
            }

            UpdateContractAndStatus(contract, contractUpdateDto);
            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();

            _ = contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients

            var contractResponse = await Mapper.MapToContractResponseDTO(contract);
            return ServiceResult<ContractResponseDTO>.Success(contractResponse);
        }


        public async Task<ServiceResult<ContractResponseDTO>> ActivateRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            var validationResult = await contractUpdateValidator.ValidateAsync(contractUpdateDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                logger.LogError("Contract update validation failed: {Errors}", errorMessages);
                return ServiceResult<ContractResponseDTO>.Fail(errorMessages, ErrorType.ValidationError);
            }

            var contract = await contractRepository.GetById(id);
            if (contract == null)
            {
                logger.LogWarning("Contract with ID {ContractId} not found", id);
                return ServiceResult<ContractResponseDTO>.Fail("Contract not found", ErrorType.NotFound);
            }

            UpdateContractAndStatus(contract, contractUpdateDto);
            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();

            _ = contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients

            var contractResponse = await Mapper.MapToContractResponseDTO(contract);
            return ServiceResult<ContractResponseDTO>.Success(contractResponse);
        }


        public async Task<ServiceResult<ContractResponseDTO>> CompleteRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            var validationResult = await contractUpdateValidator.ValidateAsync(contractUpdateDto);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                logger.LogError("Contract update validation failed: {Errors}", errorMessages);
                return ServiceResult<ContractResponseDTO>.Fail(errorMessages, ErrorType.ValidationError);
            }

            var contract = await contractRepository.GetById(id);
            if (contract == null)
            {
                logger.LogWarning("Contract with ID {ContractId} not found", id);
                return ServiceResult<ContractResponseDTO>.Fail("Contract not found", ErrorType.NotFound);
            }

            UpdateContractAndStatus(contract, contractUpdateDto);
            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();

            _ = contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients

            var contractResponse = await Mapper.MapToContractResponseDTO(contract);
            return ServiceResult<ContractResponseDTO>.Success(contractResponse);
        }

        #endregion

        #region Helpers

        private async Task HydrateContract(Contract contract)
        {
            contract.Item = await itemRepository.Get(contract.ItemId);
            contract.Owner = await userRepository.Get(contract.OwnerId);
            contract.Borrower = await userRepository.Get(contract.BorrowerId);
        }

        private void UpdateContractAndStatus(Contract contract, ContractUpdateDTO contractUpdateDto)
        {
            contract.ItemId = contractUpdateDto.ItemId;
            contract.OwnerId = contractUpdateDto.OwnerId;
            contract.BorrowerId = contractUpdateDto.BorrowerId;
            contract.StartTime = contractUpdateDto.StartTime;
            contract.EndTime = contractUpdateDto.EndTime;

            contract.StatusChanges.Add(new ContractStatusChange
            {
                ChangedAt = contractUpdateDto.UpdatedAt,
                ChangedById = contractUpdateDto.UpdatedByUserId,
                From = contractUpdateDto.FromStatus,
                To = contractUpdateDto.ToStatus,
            });

            if (contract.StatusChanges.LastOrDefault().To is ContractStatus.Accepted)
            {
                contract.Item.Status = ItemStatus.Unavailable;
            }

            if (contract.StatusChanges.LastOrDefault().To is ContractStatus.Completed or ContractStatus.Cancelled)
            {
                contract.Item.Status = ItemStatus.Available;
            }
        }


        public async Task UpdateContractStatus(Contract contract, ContractStatus toStatus, string userId)
        {
            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedById = userId,
                From = contract.StatusChanges.LastOrDefault()?.To ?? ContractStatus.None,
                To = toStatus
            });

            if (contract.StatusChanges.LastOrDefault().To is ContractStatus.Accepted)
            {
                contract.Item.Status = ItemStatus.Unavailable;
            }

            if (contract.StatusChanges.LastOrDefault().To is ContractStatus.Completed or ContractStatus.Cancelled)
            {
                contract.Item.Status = ItemStatus.Available;
            }
        }


        public async Task<bool> IsAlreadyBorrowed(int itemId)
        {
            var contract = await contractRepository.GetActiveForItem(itemId);
            if (contract.Any())
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
