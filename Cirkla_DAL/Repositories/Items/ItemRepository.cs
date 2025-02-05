using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.Items
{
    public class ItemRepository(AppDbContext context) : IItemRepository
    {
        public async Task<Item> Create(Item item)
        {
            await context.AddAsync(item);
            return item;
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await context.Items
                .Include(i => i.Pictures)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllByOwnerId(string ownerId)
        {
            return context.Items
                .Include(i => i.Pictures)
                .Where(i => i.OwnerId == ownerId)
                .OrderBy(i => i.Name)
                .ToList();
        }

        public async Task<Item?> Get(int id)
        {
            return await context.Items
                .Include(i => i.Pictures)
                .Include(i => i.Owner)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Item> Delete(Item item)
        {
            context.Remove(item);
            return Task.FromResult(item);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public async Task<Item> Update(Item item)
        {
            context.Update(item);
            return await Task.FromResult(item);
        }
    }
}
