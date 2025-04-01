using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models
{
    public class ItemPicture
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "The URL cannot exceed 2000 characters.")]
        public string Url { get; set; }

        public int? ItemId { get; set; }

        public Item? Item { get; set; }
    }
}