using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;

namespace Mapping.DTOs.Contracts
{
    public class ContractUpdateDTO : ContractCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UpdatedByUserId { get; set; }
        public ContractStatus LastStatus { get; set; } // Previous status, change name to clarify
    }
}
