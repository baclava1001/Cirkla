using Cirkla_DAL;
using Cirkla_DAL.Models.ItemPictures;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.ItemPictures
{
    public class ItemPictureRepository : IItemPictureRepository
    {
        private protected AppDbContext _context;

        public ItemPictureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ItemPicture> AddAsync(ItemPicture itemPicture)
        {
            await _context.AddAsync(itemPicture);
            return itemPicture;
        }

        public async Task<IEnumerable<ItemPicture>> GetAllAsync()
        {
            return await _context.ItemPictures.ToListAsync();
        }

        public async Task<ItemPicture> GetByIdAsync(int id)
        {
            return await _context.ItemPictures.FindAsync(id);
        }

        public Task<ItemPicture> Remove(ItemPicture itemPicture)
        {
            _context.Remove(itemPicture);
            return Task.FromResult(itemPicture);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<ItemPicture> Update(ItemPicture itemPicture)
        {
            _context.Update(itemPicture);
            return Task.FromResult(itemPicture);
        }
    }
}
