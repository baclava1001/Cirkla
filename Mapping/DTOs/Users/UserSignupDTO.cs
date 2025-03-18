using System.ComponentModel.DataAnnotations;

namespace Mapping.DTOs.Users
{
    public class UserSignupDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Url]
        public string? ProfilePictureURL { get; set; }
    }
}
