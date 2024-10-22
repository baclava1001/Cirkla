using Cirkla_DAL;
using Cirkla_DAL.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item> Add(Item item)
        {
            await _context.AddAsync(item);
            return item;
        }

        // TODO: Include only foreign keys..?
        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items
                .Include(i => i.Pictures)
                .Include(i => i.OwnerId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetAllItems(string ownerId)
        {
            return _context.Items
                .Include(i => i.Pictures)
                .Where(i => i.OwnerId == ownerId)
                .OrderBy(i => i.Name)
                .ToList();
        }

        public async Task<Item> GetItem(int id)
        {
            return _context.Items
                .Include(i => i.Pictures)
                .Include(i => i.Owner)
                .FirstOrDefault(i => i.Id == id);
        }

        public Task<Item> Remove(Item item)
        {
            _context.Remove(item);
            return Task.FromResult(item);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Item> Update(Item item)
        {
            _context.Update(item);
            return await Task.FromResult(item);
        }
    }
}
