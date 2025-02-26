using System.ComponentModel.DataAnnotations;


namespace Cirkla_DAL.Models
{
    public class Contract
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Item Item { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public User Borrower { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public List<ContractStatusChange>? StatusChanges { get; set; }
    }
}