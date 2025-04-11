using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cirkla_DAL.Repositories.UoW
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesWithTransaction()
        {
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                int result = await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
