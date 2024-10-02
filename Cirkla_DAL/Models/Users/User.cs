using Cirkla_DAL.Models.Items;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models.Users
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Url]
        public string? ProfilePictureURL { get; set; }
        public List<Item>? Items { get; set; }
        // public List<Circle>? Circles { get; set; }
    }
}
