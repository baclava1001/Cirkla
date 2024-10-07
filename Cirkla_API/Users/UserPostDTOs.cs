using System.ComponentModel.DataAnnotations;

namespace Cirkla_API.Users
{
    public class UserPostDTOs : UserLoginDTO
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

    }
}
