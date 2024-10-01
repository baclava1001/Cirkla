using Cirkla_DAL.Models.Items;

namespace Cirkla_DAL.Models.Users
{
    public class User
    {
        // TODO: Add attributes
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePictureURL { get; set; }
        public List<Item>? Items { get; set; }
        // public List<Circle>? Circles { get; set; }
    }
}
