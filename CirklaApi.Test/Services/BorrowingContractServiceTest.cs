using Cirkla_API.Common.Constants;
using Cirkla_API.Services.BorrowingContracts;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Mapping.DTOs.Contracts;
using Mapping.Mappers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Test.CirklaApi.MockDataGeneration;

namespace Test.CirklaApi.Services
{
    public class BorrowingContractServiceTest
    {
        private readonly Mock<IContractRepository> _contractRepoMock = new();
        private readonly Mock<IItemRepository> _itemRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<ILogger<BorrowingContractService>> _loggerMock = new();
        private readonly BorrowingContractService _borrowingContractService;

        private Contract contract;
        private ContractCreateDTO contractCreateDTO;
        private ContractReplyDTO contractReplyDTO;


        public BorrowingContractServiceTest()
        {
            _borrowingContractService = new BorrowingContractService(
                _contractRepoMock.Object,
                _itemRepoMock.Object,
                _userRepoMock.Object,
                _loggerMock.Object
            );
        }


        #region SendRequestTests

        [Fact]
        public async Task SendRequest_ReturnsSuccess_WhenContractIsValid()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);
            result.Payload.Id = contract.Id; // Fake EF Core Id assignment

            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Payload);
            Assert.Equal(JsonSerializer.Serialize(contract), JsonSerializer.Serialize(result.Payload));
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenStartDateIsBeforeEndDate()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.RequestInvalidStartDateBeforeEndDate);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);


            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.ValidationError, result.Error);
            Assert.Equal("Start date cannot be later than end date", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenStartDateIsBeforeCreatedDate()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.RequestInvalidStartDateBeforeCreatedDate);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);


            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.ValidationError, result.Error);
            Assert.Equal("Start date cannot be earlier than request creation date", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenItemIsNull()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);
            contract.Item = null;

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item?.Id ?? 0,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync((Item)null);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.NotFound, result.Error);
            Assert.Equal("Invalid contract details", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenOwnerIsNull()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);
            contract.Owner = null;

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner?.Id ?? string.Empty,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync((User)null);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.NotFound, result.Error);
            Assert.Equal("Invalid contract details", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }

        [Fact]
        public async Task SendRequest_ReturnsError_WhenBorrowerIsNull()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);
            contract.Borrower = null;

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower?.Id ?? string.Empty,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync((User)null);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.NotFound, result.Error);
            Assert.Equal("Invalid contract details", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenContractRepositoryCreateThrowsDbUpdateException()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ThrowsAsync(new DbUpdateException("Database update error"));
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("An error occurred while creating the contract", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenContractRepositoryCreateThrowsException()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ThrowsAsync(new Exception("Database error"));
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("Internal server error", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task SendRequest_ReturnsError_WhenSaveChangesFails()
        {
            // Arrange
            contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);

            contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created
            };

            _itemRepoMock.Setup(r => r.Get(contractCreateDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractCreateDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).ThrowsAsync(new Exception("Save changes error"));

            // Act
            var result = await _borrowingContractService.SendRequest(contractCreateDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("Internal server error", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        }

        #endregion



        #region ViewRequestSummaryTests

        [Fact]
        public async Task ViewRequestSummary_ReturnsSuccess_WhenContractExists()
        {
            // Arrange
            var contractId = 1;
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Request);
            _contractRepoMock.Setup(r => r.GetById(contractId)).ReturnsAsync(contract);

            // Act
            var result = await _borrowingContractService.ViewRequestSummary(contractId);

            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Payload);
            Assert.Equal(JsonSerializer.Serialize(contract), JsonSerializer.Serialize(result.Payload));
            _contractRepoMock.Verify(r => r.GetById(contractId), Times.Once);
        }


        [Fact]
        public async Task ViewRequestSummary_ReturnsError_WhenContractDoesNotExist()
        {
            // Arrange
            var contractId = 1;
            _contractRepoMock.Setup(r => r.GetById(contractId)).ReturnsAsync((Contract)null);

            // Act
            var result = await _borrowingContractService.ViewRequestSummary(contractId);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.NotFound, result.Error);
            Assert.Equal("Borrowing contract not found", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.GetById(contractId), Times.Once);
        }


        [Fact]
        public async Task ViewRequestSummary_ReturnsError_WhenRepositoryThrowsException()
        {
            // Arrange
            var contractId = 1;
            _contractRepoMock.Setup(r => r.GetById(contractId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _borrowingContractService.ViewRequestSummary(contractId);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("Internal server error", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.GetById(contractId), Times.Once);
        }

        #endregion



        #region RespondToRequest

        [Fact]
        public async Task RespondToRequest_ReturnsSuccess_WhenContractIsAccepted()
        {
            // Arrange
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Accepted);

            contractReplyDTO = new ContractReplyDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created,
                AcceptedByOwner = contract.AcceptedByOwner,
                DeniedByOwner = contract.DeniedByOwner
            };

            _itemRepoMock.Setup(r => r.Get(contractReplyDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Update(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.RespondToRequest(contractReplyDTO.Id, contractReplyDTO);

            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Payload);
            Assert.Equal(JsonSerializer.Serialize(contract), JsonSerializer.Serialize(result.Payload));
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        }


        [Fact]
        public async Task RespondToRequest_ReturnsSuccess_WhenContractIsDenied()
        {
            // Arrange
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Denied);

            contractReplyDTO = new ContractReplyDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created,
                AcceptedByOwner = contract.AcceptedByOwner,
                DeniedByOwner = contract.DeniedByOwner
            };

            _itemRepoMock.Setup(r => r.Get(contractReplyDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Update(It.IsAny<Contract>())).ReturnsAsync(contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.RespondToRequest(contractReplyDTO.Id, contractReplyDTO);

            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Payload);
            Assert.Equal(JsonSerializer.Serialize(contract), JsonSerializer.Serialize(result.Payload));
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        }


        [Fact]
        public async Task RespondToRequest_ReturnsError_WhenContractReplyDTOIsNull()
        {
            // Arrange
            contractReplyDTO = null;

            // Act
            var result = await _borrowingContractService.RespondToRequest(1, contractReplyDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.ValidationError, result.Error);
            Assert.Equal("Reply not valid", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task RespondToRequest_ReturnsError_WhenIdDoesNotMatchContractReplyDTOId()
        {
            // Arrange
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Denied);

            contractReplyDTO = new ContractReplyDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created,
                AcceptedByOwner = contract.AcceptedByOwner,
                DeniedByOwner = contract.DeniedByOwner
            };

            // Act
            var result = await _borrowingContractService.RespondToRequest(contractReplyDTO.Id + 1, contractReplyDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.ValidationError, result.Error);
            Assert.Equal("Reply not valid", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Never);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task RespondToRequest_ReturnsError_WhenContractRepositoryUpdateThrowsDbUpdateException()
        {
            // Arrange
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Accepted);

            contractReplyDTO = new ContractReplyDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created,
                AcceptedByOwner = contract.AcceptedByOwner,
                DeniedByOwner = contract.DeniedByOwner
            };

            _itemRepoMock.Setup(r => r.Get(contractReplyDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Update(It.IsAny<Contract>())).ThrowsAsync(new DbUpdateException("Database update error"));
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.RespondToRequest(contractReplyDTO.Id, contractReplyDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("Error saving updated borrowing contract", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }


        [Fact]
        public async Task RespondToRequest_ReturnsError_WhenContractRepositoryUpdateThrowsException()
        {
            // Arrange
            var contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractState.Accepted);

            contractReplyDTO = new ContractReplyDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                Created = contract.Created,
                AcceptedByOwner = contract.AcceptedByOwner,
                DeniedByOwner = contract.DeniedByOwner
            };

            _itemRepoMock.Setup(r => r.Get(contractReplyDTO.ItemId)).ReturnsAsync(contract.Item);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.OwnerId)).ReturnsAsync(contract.Owner);
            _userRepoMock.Setup(r => r.Get(contractReplyDTO.BorrowerId)).ReturnsAsync(contract.Borrower);
            _contractRepoMock.Setup(r => r.Update(It.IsAny<Contract>())).ThrowsAsync(new Exception("Database error"));
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.RespondToRequest(contractReplyDTO.Id, contractReplyDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.InternalError, result.Error);
            Assert.Equal("Internal server error", result.ErrorMessage);
            _contractRepoMock.Verify(r => r.Update(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Never);
        }

        #endregion
    }
}