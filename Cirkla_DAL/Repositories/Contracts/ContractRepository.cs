using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Contracts
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

        public async Task<IEnumerable<Contract>> GetAllContracts()
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetIncomingRequestsForInbox(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Owner.Id == userId)
                .Where(c => c.AcceptedByOwner == null && c.DeniedByOwner == null)
                .OrderByDescending(c => c.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetUsersPendingRequests(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Borrower.Id == userId)
                .Where(c => c.AcceptedByOwner == null && c.DeniedByOwner == null && c.EndTime < DateTime.Now)
                .OrderByDescending(c => c.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetUsersAnsweredRequests(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Borrower.Id == userId)
                .Where(c => c.AcceptedByOwner != null || c.DeniedByOwner != null && c.EndTime < DateTime.Now)
                .OrderByDescending(c => c.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetUsersRequestHistory(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Borrower.Id == userId)
                .Where(c => c.AcceptedByOwner != null || c.DeniedByOwner != null && c.EndTime > DateTime.Now)
                .OrderByDescending(c => c.Created)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetUsersContractHistory(string userId)
        {
            return await _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Owner.Id == userId)
                .Where(c => c.AcceptedByOwner != null || c.DeniedByOwner != null && c.EndTime > DateTime.Now)
                .OrderByDescending(c => c.Created)
                .ToListAsync();
        }

        public async Task<Contract> GetContract(int id)
        {
            return _context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
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
