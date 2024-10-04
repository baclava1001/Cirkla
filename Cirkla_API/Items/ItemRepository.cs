using Cirkla_DAL;
using Cirkla_DAL.Models.Items;
using Cirkla_DAL.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Items
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item> AddAsync(Item item)
        {
            await _context.AddAsync(item);
            return item;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.Include(i => i.Pictures).Include(i => i.Owner)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return _context.Items.Include(i => i.Pictures).Include(i => i.Owner).FirstOrDefault(i => i.Id == id);
        }

        public Task<Item> Remove(Item item)
        {
            _context.Remove(item);
            return Task.FromResult(item);
        }

        public async Task SaveChangesAsync()
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
