using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.DTOs.Contracts;

namespace Mapping.Mappers
{

    // TODO: Make mapper static, not a service - and send in whole objects as arguments
    // TODO: Fix user inheriting IdentityUser
    // TODO: Separate mappers for each Model
    public static class Mapper
    {
        public static async Task<User> MapUserPostDtoToUser(UserSignupDTO userSignupDto)
        {
            return new User
            {
                UserName = userSignupDto.Email,
                Email = userSignupDto.Email,
                FirstName = userSignupDto.FirstName,
                LastName = userSignupDto.LastName,
                Address = userSignupDto.Address,
                ZipCode = userSignupDto.ZipCode,
                ProfilePictureURL = userSignupDto.ProfilePictureURL,
                EmailConfirmed = true
            };
        }


        public static async Task<Contract> MapContractCreateDtoToContract(ContractCreateDTO contractCreateDTO)
        {
            return new Contract
            {
                // Id set by EF Core
                StartTime = contractCreateDTO.StartTime,
                EndTime = contractCreateDTO.EndTime,
                Created = contractCreateDTO.Created
            };
        }

        public static async Task<Contract> MapContractReplyDtoToContract(ContractReplyDTO contractReplyDTO)
        {
            return new Contract
            {
                Id = contractReplyDTO.Id,
                StartTime = contractReplyDTO.StartTime,
                EndTime = contractReplyDTO.EndTime,
                Created = contractReplyDTO.Created,
                DeniedByOwner = contractReplyDTO.DeniedByOwner,
                AcceptedByOwner = contractReplyDTO.AcceptedByOwner
            };
        }
    }
}
