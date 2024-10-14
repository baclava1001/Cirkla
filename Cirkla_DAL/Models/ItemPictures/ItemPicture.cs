using Cirkla_DAL.Models.Items;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models.ItemPictures
{
    public class ItemPicture
    {
        [Required]
        public int Id { get; set; }
        //[Url]
        public string Url { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}