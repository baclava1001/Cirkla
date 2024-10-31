using Cirkla_DAL.Models.Users;
using Cirkla_DAL.Models.Items;
using Cirkla_API.DTOs.Users;
using Cirkla_DAL.Models.Contract;
using Cirkla_API.DTOs.Contracts;
using Cirkla_API.Services;
using Cirkla_API.Repositories;

namespace Cirkla_API.Helpers
{

    // TODO: Make mapper static, not a service - and send in whole objects as arguments
    public class Mapper : IMapper
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        public Mapper(IItemRepository itemRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        #region users
        public async Task<User> MapUserPostDtoToUser(UserPostDTO userPostDTO)
        {
            return new User
            {
                UserName = userPostDTO.Email,
                Email = userPostDTO.Email,
                FirstName = userPostDTO.FirstName,
                LastName = userPostDTO.LastName,
                Address = userPostDTO.Address,
                ZipCode = userPostDTO.ZipCode,
                ProfilePictureURL = userPostDTO.ProfilePictureURL,
                EmailConfirmed = true
            };
        }
        #endregion users

        #region contracts
        public async Task<Contract> MapContractCreateDtoToContract(ContractCreateDTO contractCreateDTO)
        {
            return new Contract
            {
                // Id set by EF Core
                Item = await _itemRepository.GetItem(contractCreateDTO.ItemId),
                Owner = await _userRepository.Get(contractCreateDTO.OwnerId),
                Borrower = await _userRepository.Get(contractCreateDTO.BorrowerId),
                StartTime = contractCreateDTO.StartTime,
                EndTime = contractCreateDTO.EndTime,
                Created = contractCreateDTO.Created
            };
        }

        public async Task<Contract> MapContractReplyDtoToContract(ContractReplyDTO contractReplyDTO)
        {
            return new Contract
            {
                Id = contractReplyDTO.Id,
                Item = await _itemRepository.GetItem(contractReplyDTO.ItemId),
                Owner = await _userRepository.Get(contractReplyDTO.OwnerId),
                Borrower = await _userRepository.Get(contractReplyDTO.BorrowerId),
                StartTime = contractReplyDTO.StartTime,
                EndTime = contractReplyDTO.EndTime,
                Created = contractReplyDTO.Created,
                DeniedByOwner = contractReplyDTO.DeniedByOwner,
                AcceptedByOwner = contractReplyDTO.AcceptedByOwner
            };
        }

        #endregion contracts
    }
}
