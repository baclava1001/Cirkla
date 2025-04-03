using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Services.ContractNotifications;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.UoW;
using FluentValidation;
using Mapping.DTOs.Contracts;
using Mapping.Mappers;

namespace Cirkla_API.Services.BorrowingContracts
{
    public class BorrowingContractService(
        IContractRepository contractRepository,
        IContractNotificationService contractNotificationService,
        IUnitOfWork unitOfWork,
        IValidator<ContractCreateDTO> contractCreateValidator,
        IValidator<ContractUpdateDTO> contractUpdateValidator,
        ILogger<BorrowingContractService> logger) : IBorrowingContractService
    
    {
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
            await UpdateContractStatus(contract, ContractStatus.Pending, contract.BorrowerId);

            await contractRepository.Create(contract);
            await unitOfWork.SaveChangesWithTransaction();

            contract = await contractRepository.GetById(contract.Id);
            _ = contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            
            return ServiceResult<int>.Created(contract.Id);
        }


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


        #region Helpers

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
        }

        #endregion

    }
}
