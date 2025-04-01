using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cirkla_DAL.Models.Enums;

namespace Cirkla_DAL.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        public string? Category { get; set; }

        [StringLength(100, ErrorMessage = "Model cannot exceed 50 characters.")]
        public string? Model { get; set; }

        [StringLength(1000, ErrorMessage = "Specifications cannot exceed 500 characters.")]
        public string? Specifications { get; set; }

        [StringLength(2000, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
        // public OwnersTerms { get; set; } // Fkey for coming feature
        // public string List<HashTag> HashTags { get; set; } // Fkey for coming feature
        
        [Required]
        public ItemStatus Status { get; set; } = ItemStatus.Available;
        
        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User? Owner { get; set; }
        // public List<Circle> Circles { get; set; } // Fkey for coming feature
        
        public List<ItemPicture>? Pictures { get; set; }
    }
}