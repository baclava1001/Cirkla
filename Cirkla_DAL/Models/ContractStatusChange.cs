using Cirkla_DAL.Models.Enums;

namespace Cirkla_DAL.Models
{
    public class ContractStatusChange
    {
        public int Id { get; set; }
        public Contract Contract { get; set; }
        public DateTime ChangedAt { get; set; }
        public User? ChangedBy { get; set; }
        public ContractStatus From { get; set; }
        public ContractStatus To { get; set; }
    }
}