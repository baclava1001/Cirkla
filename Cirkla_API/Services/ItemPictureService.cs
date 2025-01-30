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


        public async Task<bool> Create(ItemPicture itemPicture)
        {
            if (itemPicture is null)
            {
                return false;
            }
            await _itemPictureRepository.Create(itemPicture);
            await _itemPictureRepository.SaveChanges();
            return true;
        }


        public async Task<bool> Update(int id, ItemPicture itemPicture)
        {
            if (itemPicture is null || id != itemPicture.Id)
            {
                return false;
            }
            await _itemPictureRepository.Update(itemPicture);
            await _itemPictureRepository.SaveChanges();
            return true;
        }


        public async Task<bool> DeleteItemPicture(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetById(id);

            if (itemPicture is null)
            {
                // return NotFound("Can not find picture at this time.");
            }
            await _itemPictureRepository.Delete(itemPicture);
            await _itemPictureRepository.SaveChanges();
            return true;
        }


        public async Task<ItemPicture> GetById(int id)
        {
            ItemPicture itemPicture = await _itemPictureRepository.GetById(id);

            if (itemPicture is null)
            {
                //return NotFound("No picture found.");
            }
            return itemPicture;
        }


        // Gets all images belonging to a specific item
        public async Task<IEnumerable<ItemPicture>> GetAllPicturesForItem(int itemId)
        {
            IEnumerable<ItemPicture> itemPictureList = await _itemPictureRepository.GetAll(itemId);

            if (!itemPictureList.Any())
            {
                // return NotFound("No picture found.");
            }
            return itemPictureList;
        }
    }
}
