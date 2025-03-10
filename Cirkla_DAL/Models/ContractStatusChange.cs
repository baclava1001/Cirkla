using Cirkla_DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models
{
    public class ContractStatusChange
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Contract Contract { get; set; }
        [Required]
        public DateTime ChangedAt { get; set; }
        public User? ChangedBy { get; set; }
        [Required]
        public ContractStatus From { get; set; }
        [Required]
        public ContractStatus To { get; set; }
    }
}