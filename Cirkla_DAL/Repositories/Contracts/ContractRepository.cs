using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Contracts
{
    public class ContractRepository(AppDbContext context) : IContractRepository
    {

        public async Task<Contract> Create(Contract contract)
        {
            await context.AddAsync(contract);
            return contract;
        }

        public async Task<IEnumerable<Contract>> GetAll()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .ToListAsync();
        }

        public async Task<Contract?> GetById(int id)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contract?> Update(Contract contract)
        {
            context.Update(contract);
            return await Task.FromResult(contract);
        }

        public async Task<Contract?> Delete(Contract contract)
        {
            context.Remove(contract);
            return await Task.FromResult(contract);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }


        // Special queries with filtering
        public async Task<IEnumerable<Contract>> GetActiveWhereUserIsBorrower(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.Status)
                .Where(c => c.Borrower.Id == userId && c.Status.LastOrDefault().To != ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetActiveWhereUserIsOwner(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.Status)
                .Where(c => c.Owner.Id == userId && c.Status.LastOrDefault().To != ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetArchivedWhereUsersWasBorrower(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Borrower.Id == userId && c.Status.LastOrDefault().To == ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetArchivedWhereUserWasOwner(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Where(c => c.Owner.Id == userId && c.Status.LastOrDefault().To == ContractStatus.Archived)
                .ToListAsync();
        }
    }
}
