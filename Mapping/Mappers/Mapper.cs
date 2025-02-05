using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.DTOs.Contracts;

namespace Mapping.Mappers
{
    // TODO: Make separate mappers for each Model
    // TODO: Turn into a service instead?
    public static class Mapper
    {
        public static async Task<User> MapToUser(UserSignupDTO userSignupDto)
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


        public static async Task<Contract> MapToContract(ContractCreateDTO contractCreateDTO, Item item, User owner, User borrower)
        {
            return new Contract
            {
                // Id is set later by EF Core
                Item = item,
                Owner = owner,
                Borrower = borrower,
                StartTime = contractCreateDTO.StartTime,
                EndTime = contractCreateDTO.EndTime,
                Created = contractCreateDTO.Created
            };
        }


        public static async Task<Contract> MapToContract(ContractReplyDTO contractReplyDTO)
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
