using Cirkla_DAL;
using Cirkla_DAL.Models.Contract;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;

        public ContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Contract> Add(Contract contract)
        {
            await _context.AddAsync(contract);
            return contract;
        }

        // TODO: Include only foreign keys..?
        public async Task<IEnumerable<Contract>> GetAllContracts()
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllContracts(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .ToListAsync();
        }

        public async Task<Contract> GetContract(int id)
        {
            return _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task<Contract> Remove(Contract contract)
        {
            _context.Remove(contract);
            return await Task.FromResult(contract);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Contract> Update(Contract contract)
        {
            _context.Update(contract);
            return await Task.FromResult(contract);
        }
    }
}
