using Cirkla_DAL.Models.Items;

namespace Cirkla_DAL.Models.ItemPictures
{
    public class ItemPicture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}