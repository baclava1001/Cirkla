using Cirkla_DAL.Models.Users;
using Cirkla_DAL.Models.Items;
using System.ComponentModel.DataAnnotations;

namespace Cirkla_API.DTOs.Contracts
{
    /// <summary>
    /// Hides away complex objects and takes only their Id:s
    /// </summary>
    public class ContractCreateDTO
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
        public DateTimeOffset StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
    }
}
