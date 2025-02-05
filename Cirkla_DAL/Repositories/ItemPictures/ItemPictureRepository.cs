using Cirkla_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL.Repositories.ItemPictures
{
    public class ItemPictureRepository(AppDbContext context) : IItemPictureRepository
    {
        public async Task<ItemPicture> Create(ItemPicture itemPicture)
        {
            await context.AddAsync(itemPicture);
            return itemPicture;
        }

        // Gets all images related to a certain item
        public async Task<IEnumerable<ItemPicture>> GetAll(int itemId)
        {
            return await context.ItemPictures.Where(p => p.ItemId == itemId).ToListAsync();
        }

        public async Task<ItemPicture?> GetById(int id)
        {
            return await context.ItemPictures.FindAsync(id);
        }

        public Task<ItemPicture> Delete(ItemPicture itemPicture)
        {
            context.Remove(itemPicture);
            return Task.FromResult(itemPicture);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public Task<ItemPicture> Update(ItemPicture itemPicture)
        {
            context.Update(itemPicture);
            return Task.FromResult(itemPicture);
        }
    }
}
