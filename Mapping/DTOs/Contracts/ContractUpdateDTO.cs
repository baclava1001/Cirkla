using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;

namespace Mapping.DTOs.Contracts
{
    public class ContractUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string BorrowerId { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string UpdatedByUserId { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        [Required]
        public ContractStatus FromStatus { get; set; }
        [Required]
        public ContractStatus ToStatus { get; set; }
    }
}
