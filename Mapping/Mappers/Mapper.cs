using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Mapping.DTOs.Users;
using Mapping.DTOs.Contracts;

namespace Mapping.Mappers
{
    // TODO: Make separate mappers for each Model
    // TODO: Turn into a service with interface instead?
    
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
            var contract = new Contract
            {
                Item = item,
                Owner = owner,
                Borrower = borrower,
                Created = contractCreateDTO.Created,
                StartTime = contractCreateDTO.StartTime,
                EndTime = contractCreateDTO.EndTime,
                StatusChanges = new List<ContractStatusChange>
                {
                    new ContractStatusChange
                    {
                        ChangedAt = contractCreateDTO.Created,
                        ChangedBy = borrower,
                        From = ContractStatus.None,
                        To = contractCreateDTO.CurrentStatus,
                    }
                }
            };
            return contract;
        }


        public static async Task<Contract> MapToContract(ContractUpdateDTO contractUpdateDTO, Item item, User owner, User borrower, User updatingUser)
        {
            var contract = new Contract()
            {
                Id = contractUpdateDTO.Id,
                Item = item,
                Owner = owner,
                StartTime = contractUpdateDTO.StartTime,
                EndTime = contractUpdateDTO.EndTime,
                StatusChanges = new List<ContractStatusChange>()
            };

            contract.StatusChanges.Add(new ContractStatusChange
            {
                ChangedAt = contractUpdateDTO.Created,
                ChangedBy = updatingUser,
                From = contractUpdateDTO.LastStatus,
                To = contractUpdateDTO.CurrentStatus
            });

            return contract;
        }
    }
}
