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
using Test.CirklaApi.MockDataGeneration;

namespace Test.CirklaApi.Services
{
    public class BorrowingContractServiceTest
    {
        // Done:
        // 1. SendRequest_WhenContractDTOFromClient_ReturnsServiceResultContract

        //What to test:
        // 2. SendRequest_WhenStartTimeIsLaterThanEndTime_ReturnsError
        // 3. SendRequest_WhenItemDoesNotExist_ReturnsError
        // 4. SendRequest_WhenOwnerDoesNotExist_ReturnsError
        // 5. SendRequest_WhenBorrowerDoesNotExist_ReturnsError
        // 6. SendRequest_WhenContractRepositoryCreateThrowsException_ReturnsError
        // 7. SendRequest_

        private readonly Mock<IContractRepository> _contractRepoMock = new();
        private readonly Mock<IItemRepository> _itemRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<ILogger<BorrowingContractService>> _loggerMock = new();
        private readonly BorrowingContractService _borrowingContractService;


        private readonly ContractCreateDTO _contractDTO;
        private readonly Contract _contract;


        public BorrowingContractServiceTest()
        {
            _borrowingContractService = new BorrowingContractService(
                _contractRepoMock.Object,
                _itemRepoMock.Object,
                _userRepoMock.Object,
                _loggerMock.Object
            );

            _contract = FakeDataGenerator.GenerateContract(FakeDataGenerator.ContractType.Request);

            _contractDTO = new ContractCreateDTO
            {
                ItemId = _contract.Item.Id,
                OwnerId = _contract.Owner.Id,
                BorrowerId = _contract.Borrower.Id,
                StartTime = _contract.StartTime,
                EndTime = _contract.EndTime,
                Created = _contract.Created
            };
        }


        [Fact]
        public async Task SendRequest_ShouldReturnSuccess_WhenContractIsValid()
        {
            // Arrange
            _itemRepoMock.Setup(r => r.Get(_contractDTO.ItemId)).ReturnsAsync(_contract.Item);
            _userRepoMock.Setup(r => r.Get(_contractDTO.OwnerId)).ReturnsAsync(_contract.Owner);
            _userRepoMock.Setup(r => r.Get(_contractDTO.BorrowerId)).ReturnsAsync(_contract.Borrower);

            _contractRepoMock.Setup(r => r.Create(It.IsAny<Contract>())).ReturnsAsync(_contract);
            _contractRepoMock.Setup(r => r.SaveChanges()).Returns(Task.CompletedTask);

            // Act
            var result = await _borrowingContractService.SendRequest(_contractDTO);
            result.Payload.Id = _contract.Id; // Fake EF Core Id assignment

            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Payload);
            Assert.Equal(JsonSerializer.Serialize(_contract), JsonSerializer.Serialize(result.Payload));
            _contractRepoMock.Verify(r => r.Create(It.IsAny<Contract>()), Times.Once);
            _contractRepoMock.Verify(r => r.SaveChanges(), Times.Once);
        }


        [Fact]
        public async Task SendRequest_WhenStartTimeIsLaterThanEndTime_ReturnsError()
        {
            // Arrange

            // Act
            var result = await _borrowingContractService.SendRequest(_contractDTO);

            // Assert
            Assert.True(result.IsError);
            Assert.Equal(ErrorType.ValidationError, result.Error);
            Assert.Equal("Start time cannot be later than end time.", result.ErrorMessage);
        }


        //[Fact]
        //public async Task SendRequest_WhenItemDoesNotExist_ReturnsError()
        //{
        //    // Arrange
        //    var contractDTO = new ContractCreateDTO
        //    {
        //        ItemId = 12,
        //        OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
        //        BorrowerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
        //        StartTime = DateTime.UtcNow.AddDays(2),
        //        EndTime = DateTime.UtcNow.AddDays(9)
        //    };

        //    _itemRepoMock.Setup(r => r.Get(12)).ReturnsAsync((Item)null);

        //    // Act
        //    var result = await _borrowingContractService.SendRequest(contractDTO);

        //    // Assert
        //    Assert.True(result.IsError);
        //    Assert.Equal(ErrorType.NotFound, result.Error);
        //    Assert.Equal("Item does not exist.", result.ErrorMessage);
        //}

    }
}