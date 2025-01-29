using System.ComponentModel.DataAnnotations;

namespace Mapping.DTOs.Users
{
    public class UserSignupDTO : UserLoginDTO
    {
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

        // Inherits from UserLoginDTO:
        // string Email
        // string password
    }
}
