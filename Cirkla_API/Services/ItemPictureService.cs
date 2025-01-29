using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.ItemPictures;

namespace Cirkla_API.Services
{
    public class ItemPictureService : IItemPictureService
    {
        IItemPictureRepository _itemPictureRepository;

        public ItemPictureService(IItemPictureRepository itemPictureRepository)
        {
            _itemPictureRepository = itemPictureRepository;
        }

        public async Task<bool> AddItemPictureAsync(ItemPicture itemPicture)
        {
            if (itemPicture is null)
            {
                return false;
            }
            await _itemPictureRepository.AddAsync(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeItemPicture(int id, ItemPicture itemPicture)
        {
            if (itemPicture is null || id != itemPicture.Id)
            {
                return false;
            }
            await _itemPictureRepository.Update(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemPicture(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetByIdAsync(id);

            if (itemPicture is null)
            {
                // return NotFound("Can not find picture at this time.");
            }
            await _itemPictureRepository.Remove(itemPicture);
            await _itemPictureRepository.SaveChangesAsync();
            return true;
        }

        public async Task<ItemPicture> FindItemPictureByIdAsync(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetByIdAsync(id);

            if (itemPicture is null)
            {
                //return NotFound("No picture found.");
            }
            return itemPicture;
        }

        // Gets all images belonging to a specific item
        public async Task<IEnumerable<ItemPicture>> ListItemPicturesAsync(int itemId)
        {
            IEnumerable<ItemPicture> itemPictureList = await _itemPictureRepository.GetAllAsync(itemId);

            if (!itemPictureList.Any())
            {
                // return NotFound("No picture found.");
            }
            return itemPictureList;
        }
    }
}
