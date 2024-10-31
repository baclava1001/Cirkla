using Cirkla_API.Repositories;
using Cirkla_DAL.Models.Contract;
using Cirkla_API.DTOs.Contracts;
using Cirkla_API.Helpers;

namespace Cirkla_API.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IMapper _mapper;
        private readonly IContractRepository _contractRepository;

        public BorrowingService(IMapper mapper, IContractRepository contractRepository)
        {
            _mapper = mapper;
            _contractRepository = contractRepository;
        }

        public async Task<Contract> AskForItem(ContractCreateDTO contractDTOFromClient)
        {
            Contract contract = await _mapper.MapContractCreateDtoToContract(contractDTOFromClient);

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

        public async Task<IEnumerable<Contract>> GetRequestsForInbox(string userId)
        {
            IEnumerable<Contract> contracts = await _contractRepository.GetAllContracts(userId);
            return contracts;
        }

        public async Task<Contract> RespondToRequest(int id, ContractReplyDTO contractReplyDTO)
        {
            if (contractReplyDTO is null || id != contractReplyDTO.Id)
            {
                throw new NullReferenceException();
            }

            Contract contract = await _mapper.MapContractReplyDtoToContract(contractReplyDTO);

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
