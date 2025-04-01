using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models
{
    public class Circle
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }
        
        [Required]
        public bool IsPublic { get; set; }
        
        public List<User> Administrators { get; set; } // All administrators are also members
        
        public List<User> Members { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public string? CreatedById { get; set; }
        
        public User? CreatedBy { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        public string? UpdatedById { get; set; }
        
        public User? UpdatedBy { get; set; }
    }
}