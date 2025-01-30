using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.ItemPictures
{
    public class ItemPictureRepository : IItemPictureRepository
    {
        private protected AppDbContext _context;

        public ItemPictureRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ItemPicture> Create(ItemPicture itemPicture)
        {
            await _context.AddAsync(itemPicture);
            return itemPicture;
        }

        // Gets all images related to a certain item
        public async Task<IEnumerable<ItemPicture>> GetAll(int itemId)
        {
            return await _context.ItemPictures.Where(p => p.ItemId == itemId).ToListAsync();
        }

        public async Task<ItemPicture> GetById(int id)
        {
            return await _context.ItemPictures.FindAsync(id);
        }

        public Task<ItemPicture> Delete(ItemPicture itemPicture)
        {
            _context.Remove(itemPicture);
            return Task.FromResult(itemPicture);
        }

        public async Task SaveChanges()
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
