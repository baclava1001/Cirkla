using Cirkla_DAL.Models.ItemPictures;
using Cirkla_DAL.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models.Items
{
    public class Item
    {
        // TODO: Add attributes
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
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
        // public List<Circle> Circles { get; set; } // Fkey for coming feature
        public List<ItemPicture>? Pictures { get; set; }
        // public List<Contract> Contracts { get; set; } // Fkey for coming feature
    }
}