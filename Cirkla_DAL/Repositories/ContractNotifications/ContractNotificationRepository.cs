using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.ContractNotifications;

public class ContractNotificationRepository(AppDbContext context) : IContractNotificationRepository
{
    public async Task<ContractNotification> Create(ContractNotification contractNotification)
    {
        await context.AddAsync(contractNotification);
        return contractNotification;
    }

    public async Task<IEnumerable<ContractNotification>> GetAll()
    {
        return await context.ContractNotifications
            .Include(cn => cn.Contract)
                .ThenInclude(c => c.Item)
            .Include(cn => cn.Contract)
                .ThenInclude(c => c.Owner)
            .Include(cn => cn.Contract)
                .ThenInclude(c => c.Borrower)
            .Include(cn => cn.Contract)
                .ThenInclude(c => c.StatusChanges)
            .ToListAsync();
    }

    public async Task<ContractNotification?> GetById(int id)
    {
        return await context.ContractNotifications
            .Include(cn => cn.Contract)
            .FirstOrDefaultAsync(cn => cn.Id == id);
    }

    public async Task<ContractNotification?> Update(ContractNotification contractNotification)
    {
        context.Update(contractNotification);
        return await Task.FromResult(contractNotification);
    }

    public async Task<ContractNotification?> Delete(ContractNotification contractNotification)
    {
        context.Remove(contractNotification);
        return await Task.FromResult(contractNotification);
    }

    public async Task SaveChanges()
    {
        await context.SaveChangesAsync();
    }
}