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
    }
}
