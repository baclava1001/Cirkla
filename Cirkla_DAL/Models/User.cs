using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cirkla_DAL.Models
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
        public List<Circle> AdministeredCircles { get; set; } = new List<Circle>();
        public List<Circle> MemberCircles { get; set; } = new List<Circle>();
    }
}
