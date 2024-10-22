using Cirkla_API.Repositories;
using Cirkla_DAL.Models.Contract;

namespace Cirkla_API.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IContractRepository _contractRepository;

        public BorrowingService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<Contract> AskForItem(Contract contract)
        {
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
                throw new Exception("Something went wrong");
            }
            return contract;
        }
    }
}
