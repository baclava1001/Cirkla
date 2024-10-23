using System.ComponentModel.DataAnnotations;

namespace Cirkla_API.DTOs.Users
{
    public class UserPostDTO : UserLoginDTO
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

        // From UserLoginDTO:
        // string Email
        // string password
    }
}
