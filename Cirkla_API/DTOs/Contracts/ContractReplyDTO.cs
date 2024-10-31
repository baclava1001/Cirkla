namespace Cirkla_API.DTOs.Contracts
{
    public class ContractReplyDTO : ContractCreateDTO
    {
        public int Id { get; set; }
        public DateTime? AcceptedByOwner { get; set; }
        public DateTime? DeniedByOwner { get; set; }
    }
}
