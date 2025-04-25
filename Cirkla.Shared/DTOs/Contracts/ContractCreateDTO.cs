using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models.Enums;

namespace Cirkla.Shared.DTOs.Contracts
{
    /// <summary>
    /// Hides away complex objects and takes only their Id:s
    /// </summary>
    public class ContractCreateDTO
    {
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
        public ContractStatus CurrentStatus { get; set; }
    }
}
