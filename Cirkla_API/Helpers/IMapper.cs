using Cirkla_API.DTOs.Contracts;
using Cirkla_API.DTOs.Users;
using Cirkla_DAL.Models.Contract;
using Cirkla_DAL.Models.Users;

namespace Cirkla_API.Helpers
{
    public interface IMapper
    {
        Task<User> MapUserPostDtoToUser(UserPostDTO userPostDTO);
        Task<Contract> MapContractCreateDtoToContract(ContractCreateDTO contractCreateDTO);
        Task<Contract> MapContractReplyDtoToContract(ContractReplyDTO contractReplyDTO);
    }
}