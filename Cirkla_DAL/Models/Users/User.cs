using Cirkla_DAL.Models.Items;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models.Users
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        //[Url]
        public string? ProfilePictureURL { get; set; }
        public List<Item>? Items { get; set; }

        // Future feature: public List<Circle>? Circles { get; set; }
    }
}
