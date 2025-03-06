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
                .ThenInclude(I => I.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .ToListAsync();
        }

        public async Task<Contract?> GetById(int id)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .ThenInclude(I => I.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
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
        public async Task<IEnumerable<Contract>> GetAllActive()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllCancelled()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Cancelled)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllCompleted()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Completed)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllArchived()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetReadyForArchive()
        {
            // 48 hours after cancellation or completion the contract is ready for archiving
            var twoDaysAgo = DateTime.Now.AddHours(-48);
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Completed ||
                            c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Cancelled)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().ChangedAt < twoDaysAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetActiveButLate()
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Active)
                .Where(c => c.EndTime > DateTime.Now)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetAcceptedButNotPickedUp()
        {
            // 48 hours after the agreed start time the contract will be automatically cancelled
            var twoDaysAgo = DateTime.Now.AddHours(-48);
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To == ContractStatus.Accepted)
                .Where(c => c.StartTime < twoDaysAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetActiveWhereUserIsBorrower(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.Borrower.Id == userId && c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To != ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetActiveWhereUserIsOwner(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.Owner.Id == userId && c.StatusChanges.OrderByDescending(sc => sc.ChangedAt).FirstOrDefault().To != ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetActiveForItem(int itemId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.Item.Id == itemId && c.StatusChanges.Any(sc => sc.To == ContractStatus.Active))
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetArchivedWhereUsersWasBorrower(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.Borrower.Id == userId && c.StatusChanges.LastOrDefault().To == ContractStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetArchivedWhereUserWasOwner(string userId)
        {
            return await context.Contracts
                .Include(c => c.Item)
                .Include(c => c.Item.Pictures)
                .Include(c => c.Owner)
                .Include(c => c.Borrower)
                .Include(c => c.StatusChanges)!
                .ThenInclude(sc => sc.ChangedBy)
                .Where(c => c.Owner.Id == userId && c.StatusChanges.LastOrDefault().To == ContractStatus.Archived)
                .ToListAsync();
        }
    }
}
