using Cirkla_DAL.Models.Contract;

namespace Cirkla_API.Services
{
    public interface IBorrowingService
    {
        Task<Contract> AskForItem(Contract contract);
    }
}
