using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cirkla_DAL.Models
{
    public class Contract
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item? Item { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public User? Owner { get; set; }

        [Required, ForeignKey("BorrowerId")]
        public string BorrowerId { get; set; }
        public User? Borrower { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
        
        public List<ContractStatusChange>? StatusChanges { get; set; }
    }
}