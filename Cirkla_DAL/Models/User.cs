using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cirkla_DAL.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code format.")]
        public string ZipCode { get; set; }

        public string? ProfilePictureURL { get; set; }

        public List<Item>? Items { get; set; }

        public List<Circle> AdministeredCircles { get; set; } = new List<Circle>();

        public List<Circle> MemberCircles { get; set; } = new List<Circle>();
    }
}
