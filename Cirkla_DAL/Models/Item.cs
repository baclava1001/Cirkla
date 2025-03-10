using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cirkla_DAL.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Category { get; set; }
        public string? Model { get; set; }
        public string? Specifications { get; set; }
        public string? Description { get; set; }
        // public OwnersTerms { get; set; } // Fkey for coming feature
        // public string List<HashTag> HashTags { get; set; } // Fkey for coming feature
        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User? Owner { get; set; }
        // public List<Circle> Circles { get; set; } // Fkey for coming feature
        public List<ItemPicture>? Pictures { get; set; }
    }
}