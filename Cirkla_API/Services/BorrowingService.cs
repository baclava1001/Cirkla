using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Mapping.Mappers;
using Mapping.DTOs.Contracts;

namespace Cirkla_API.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

        public BorrowingService(IContractRepository contractRepository, IItemRepository itemRepository, IUserRepository userRepository)
        {
            _contractRepository = contractRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public async Task<Contract> AskForItem(ContractCreateDTO contractDTOFromClient)
        {
            Contract contract = await Mapper.MapContractCreateDtoToContract(contractDTOFromClient);
            contract.Item = await _itemRepository.GetItem(contractDTOFromClient.ItemId);
            contract.Owner = await _userRepository.Get(contractDTOFromClient.OwnerId);
            contract.Borrower = await _userRepository.Get(contractDTOFromClient.BorrowerId);

            if (contract is null)
            {
                return null;
            }
                
            try
            {
                await _contractRepository.Add(contract);
                await _contractRepository.SaveChanges();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            return contract;
        }

        public async Task<Contract> ViewRequestSummary(int id)
        {
            Contract contract = await _contractRepository.GetContract(id);

            if (contract is null)
            {
                return null;
            }
            return contract;
        }


        // TODO: Extract methods to a separate InboxService
        public async Task<IEnumerable<Contract>> GetIncomingRequestsForInbox(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetIncomingRequestsForInbox(userId);
            return contracts;
        }

        public async Task<IEnumerable<Contract>> GetMyPendingRequests(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetUsersPendingRequests(userId);
            return contracts;
        }

        public async Task<IEnumerable<Contract>> GetMyAnsweredRequests(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetUsersAnsweredRequests(userId);
            return contracts;
        }

        public async Task<IEnumerable<Contract>> GetMyRequestHistory(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetUsersRequestHistory(userId);
            return contracts;
        }

        public async Task<IEnumerable<Contract>> GetMyContractHistory(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetUsersContractHistory(userId);
            return contracts;
        }

        public async Task<Contract> RespondToRequest(int id, ContractReplyDTO contractReplyDTO)
        {
            if (contractReplyDTO is null || id != contractReplyDTO.Id)
            {
                throw new NullReferenceException();
            }

            Contract contract = await Mapper.MapContractReplyDtoToContract(contractReplyDTO);
            contract.Item = await _itemRepository.GetItem(contractReplyDTO.ItemId);
            contract.Owner = await _userRepository.Get(contractReplyDTO.OwnerId);
            contract.Borrower = await _userRepository.Get(contractReplyDTO.BorrowerId);

            try
            {
                await _contractRepository.Update(contract);
                await _contractRepository.SaveChanges();
                return contract;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
