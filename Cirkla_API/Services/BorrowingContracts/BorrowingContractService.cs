using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Mapping.DTOs.Contracts;
using Mapping.Mappers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cirkla_API.Services.ContractNotifications;
using Cirkla_DAL.Models.Enums;
using Cirkla_DAL.Repositories.UoW;

namespace Cirkla_API.Services.BorrowingContracts
{
    public class BorrowingContractService(
        IContractNotificationService contractNotificationService,
        IContractRepository contractRepository,
        IItemRepository itemRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ILogger<BorrowingContractService> logger) : IBorrowingContractService
    {


        // Sends a request to borrow an item, by creating a new contract.
        public async Task<ServiceResult<Contract>> SendRequest(ContractCreateDTO contractDTOFromClient)
        {
            if (contractDTOFromClient is null)
            {
                logger.LogWarning("Attempted creating a new borrowing contract with null value");
                return ServiceResult<Contract>.Fail("Request not valid", ErrorType.ValidationError);
            }

            if (contractDTOFromClient.StartTime < contractDTOFromClient.Created)
            {
                logger.LogWarning("Borrowing contract start date was earlier than the date of creation");
                return ServiceResult<Contract>.Fail("Start date cannot be earlier than request creation date",
                    ErrorType.ValidationError);
            }

            if (contractDTOFromClient.StartTime > contractDTOFromClient.EndTime)
            {
                logger.LogWarning("Borrowing contract start date cannot be later than end date");
                return ServiceResult<Contract>.Fail("Start date cannot be later than end date",
                    ErrorType.ValidationError);
            }

            var item = await itemRepository.Get(contractDTOFromClient.ItemId);
            var owner = await userRepository.Get(contractDTOFromClient.OwnerId);
            var borrower = await userRepository.Get(contractDTOFromClient.BorrowerId);
            var contract = await Mapper.MapToContract(contractDTOFromClient, item, owner, borrower);
            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedBy = await userRepository.Get(contractDTOFromClient.BorrowerId),
                From = ContractStatus.None,
                To = ContractStatus.Pending
            });

            if (contract.Item is null || contract.Owner is null || contract.Borrower is null)
            {
                logger.LogWarning("Invalid contract details. One or more properties returned null.");
                return ServiceResult<Contract>.Fail("Invalid contract details", ErrorType.NotFound);
            }

            await contractRepository.Create(contract);
            await unitOfWork.SaveChangesWithTransaction();
            // TODO: Return a thin and flat DTO instead of full object
            await contractNotificationService
                .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            return ServiceResult<Contract>.Success(contract);
        }


        public async Task<ServiceResult<Contract>> ViewRequestSummary(int id)
        {
            var contract = await contractRepository.GetById(id);
            if (contract is null)
            {
                logger.LogWarning("Borrowing contract with ID {id} not found", id);
                return ServiceResult<Contract>.Fail("Borrowing contract not found", ErrorType.NotFound);
            }

            // TODO: Return a thin and flat DTO instead of full object
            return ServiceResult<Contract>.Success(contract);
        }


        // Updates a borrowing contract to respond to a request.
        public async Task<ServiceResult<Contract>> RespondToRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            if (contractUpdateDto is null || id != contractUpdateDto.Id)
            {
                logger.LogWarning(
                    "Attempted updating an borrowing contract (to reply to a request) with null value or ID mismatch");
                return ServiceResult<Contract>.Fail("Reply not valid", ErrorType.ValidationError);
            }

            // TODO: Move to a separate helper/service that fetches all necessary entities and calls mapper
            var item = await itemRepository.Get(contractUpdateDto.ItemId);
            var owner = await userRepository.Get(contractUpdateDto.OwnerId);
            var borrower = await userRepository.Get(contractUpdateDto.BorrowerId);
            var contract = await Mapper.MapToContract(contractUpdateDto, item, owner, borrower);

            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedBy = await userRepository.Get(contractUpdateDto.UpdatedByUserId),
                From = contractUpdateDto.LastStatus,
                To = contractUpdateDto.CurrentStatus
            });


            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();
            if (!ServiceResult<Contract>.Success(contract).IsError)
            {
                await contractNotificationService
                    .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            }

            // TODO: Return a thin and flat DTO instead of full object
            return ServiceResult<Contract>.Success(contract);
        }

        public async Task<ServiceResult<Contract>> CancelRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            if (contractUpdateDto is null || id != contractUpdateDto.Id)
            {
                logger.LogWarning(
                    "Attempted cancelling borrowing contract with null value or ID mismatch");
                return ServiceResult<Contract>.Fail("Unable to cancel", ErrorType.ValidationError);
            }

            var item = await itemRepository.Get(contractUpdateDto.ItemId);
            var owner = await userRepository.Get(contractUpdateDto.OwnerId);
            var borrower = await userRepository.Get(contractUpdateDto.BorrowerId);
            var contract = await Mapper.MapToContract(contractUpdateDto, item, owner, borrower);
            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedBy = await userRepository.Get(contractUpdateDto.UpdatedByUserId),
                From = contractUpdateDto.LastStatus,
                To = contractUpdateDto.CurrentStatus
            });

            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();
            if (!ServiceResult<Contract>.Success(contract).IsError)
            {
                await contractNotificationService
                    .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            }
            return ServiceResult<Contract>.Success(contract);

        }


        public async Task<ServiceResult<Contract>> ActivateRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            if (contractUpdateDto is null || id != contractUpdateDto.Id)
            {
                logger.LogWarning(
                    "Attempted activating borrowing contract with null value or ID mismatch");
                return ServiceResult<Contract>.Fail("Unable to cancel", ErrorType.ValidationError);
            }

            var item = await itemRepository.Get(contractUpdateDto.ItemId);
            var owner = await userRepository.Get(contractUpdateDto.OwnerId);
            var borrower = await userRepository.Get(contractUpdateDto.BorrowerId);
            var contract = await Mapper.MapToContract(contractUpdateDto, item, owner, borrower);
            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedBy = await userRepository.Get(contractUpdateDto.UpdatedByUserId),
                From = contractUpdateDto.LastStatus,
                To = contractUpdateDto.CurrentStatus
            });

            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();
            if (!ServiceResult<Contract>.Success(contract).IsError)
            {
                await contractNotificationService
                    .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            }
            return ServiceResult<Contract>.Success(contract);
        }


        public async Task<ServiceResult<Contract>> CompleteRequest(int id, ContractUpdateDTO contractUpdateDto)
        {
            if (contractUpdateDto is null || id != contractUpdateDto.Id)
            {
                logger.LogWarning(
                    "Attempted completing borrowing contract with null value or ID mismatch");
                return ServiceResult<Contract>.Fail("Unable to cancel", ErrorType.ValidationError);
            }

            var item = await itemRepository.Get(contractUpdateDto.ItemId);
            var owner = await userRepository.Get(contractUpdateDto.OwnerId);
            var borrower = await userRepository.Get(contractUpdateDto.BorrowerId);
            var contract = await Mapper.MapToContract(contractUpdateDto, item, owner, borrower);
            contract.StatusChanges.Add(new ContractStatusChange
            {
                Contract = contract,
                ChangedAt = DateTime.Now,
                ChangedBy = await userRepository.Get(contractUpdateDto.UpdatedByUserId),
                From = contractUpdateDto.LastStatus,
                To = contractUpdateDto.CurrentStatus
            });

            await contractRepository.Update(contract);
            await unitOfWork.SaveChangesWithTransaction();
            if (!ServiceResult<Contract>.Success(contract).IsError)
            {
                await contractNotificationService
                    .CreateNotification(contract); // Pushes notification down to db and a DTO up to clients
            }
            return ServiceResult<Contract>.Success(contract);
        }

    }
}
