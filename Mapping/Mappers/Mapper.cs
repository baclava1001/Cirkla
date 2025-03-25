using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;
using Mapping.DTOs.CircleJoinRequests;
using Mapping.DTOs.ContractNotifications;
using Mapping.DTOs.Users;
using Mapping.DTOs.Contracts;
using Mapping.DTOs.Items;
using ApiClient = Cirkla.ApiClient;


namespace Mapping.Mappers
{
    // TODO: Make separate mappers for each Model
    // TODO: Turn into a service with interface instead?
    
    public static class Mapper
    {

        #region Item

        public static async Task<Item> MapToItem(ItemCreateDTO itemCreateDTO, User owner)
        {
            return new Item
            {
                Name = itemCreateDTO.Name,
                Category = itemCreateDTO.Category,
                Model = itemCreateDTO.Model,
                Specifications = itemCreateDTO.Specifications,
                Description = itemCreateDTO.Description,
                OwnerId = itemCreateDTO.OwnerId,
                Owner = owner,
                Pictures = itemCreateDTO.Pictures
            };
        }

        public static async Task<ApiClient.ItemCreateDTO> MapToItemCreateDTO(ApiClient.Item item)
        {
            return new ApiClient.ItemCreateDTO
            {
                Name = item.Name,
                Category = item.Category,
                Model = item.Model,
                Specifications = item.Specifications,
                Description = item.Description,
                OwnerId = item.OwnerId,
                Pictures = item.Pictures?.Select(p => new ApiClient.ItemPicture
                {
                    Id = p.Id,
                    Url = p.Url,
                    ItemId = p.ItemId
                }).ToList() // Because NSwag uses ICollection instead of List
            };
        }

        #endregion

        #region User

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

        #endregion

        #region Contract
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

        public static async Task<ContractCreateDTO> MapToContractCreateDTO(Contract contract)
        {
            var contractCreateDTO = new ContractCreateDTO
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                Created = contract.Created,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                CurrentStatus = contract.StatusChanges.Last().To
            };
            return contractCreateDTO;
        }


        public static async Task<Contract> MapToContract(ContractUpdateDTO contractUpdateDTO, Item item, User owner, User borrower)
        {
            var contract = new Contract()
            {
                Id = contractUpdateDTO.Id,
                Item = item,
                Owner = owner,
                Borrower = borrower,
                StartTime = contractUpdateDTO.StartTime,
                EndTime = contractUpdateDTO.EndTime,
                StatusChanges = new List<ContractStatusChange>()
            };
            return contract;
        }


        public static async Task<ContractUpdateDTO> MapToContractUpdateDTO(Contract contract)
        {
            var contractUpdateDTO = new ContractUpdateDTO()
            {
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                Created = contract.Created,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                CurrentStatus = contract.StatusChanges.Last().To
            };
            return contractUpdateDTO;
        }

        #endregion

        #region ContractNotifications

        public static async Task<ContractNotificationForViews> MapToContractNotificationForViews(ContractNotification contractNotification)
        {
            var contractNotificationForViews = new ContractNotificationForViews
            {
                Id = contractNotification.Id,
                NotificationMessage = contractNotification.NotificationMessage,
                CreatedAt = contractNotification.CreatedAt,
                HasBeenRead = contractNotification.HasBeenRead,
                ContractId = contractNotification.Contract.Id,
                ItemName = contractNotification.Contract.Item.Name,
                OwnerFullName = contractNotification.Contract.Owner.FirstName + " " + contractNotification.Contract.Owner.LastName,
                BorrowerFullName = contractNotification.Contract.Borrower.FirstName + " " + contractNotification.Contract.Borrower.LastName,
                Created = contractNotification.Contract.Created,
                StartTime = contractNotification.Contract.StartTime,
                EndTime = contractNotification.Contract.EndTime
            };
            return contractNotificationForViews;
        }


        public static async Task<ContractNotification> MapToContractNotification(ContractNotificationForViews contractNotificationForViews, Contract contract)
        {
            var contractNotification = new ContractNotification
            {
                Id = contractNotificationForViews.Id,
                NotificationMessage = contractNotificationForViews.NotificationMessage,
                CreatedAt = contractNotificationForViews.CreatedAt,
                HasBeenRead = contractNotificationForViews.HasBeenRead,
                Contract = contract
            };
            return contractNotification;
        }

        #endregion

        #region CircleRequests

        public static async Task<CircleJoinRequest> MapToCircleRequest(CircleJoinRequestCreateDTO circleJoinRequestCreateDto, Circle circle, User targetUser, User fromUser)
        {
            var circleRequest = new CircleJoinRequest
            {
                CircleId = circleJoinRequestCreateDto.CircleId,
                Circle = circle,
                Type = circleJoinRequestCreateDto.Type,
                TargetUserId = circleJoinRequestCreateDto.TargetUserId,
                TargetUser = targetUser,
                FromUserId = circleJoinRequestCreateDto.FromUserId,
                FromUser = fromUser,
                RequestDate = circleJoinRequestCreateDto.RequestDate,
                Status = circleJoinRequestCreateDto.Status,
                ExpiresAt = circleJoinRequestCreateDto.ExpiresAt
            };
            return circleRequest;
        }

        public static async Task<ApiClient.CircleJoinRequestUpdateDTO> MapToCircleRequestUpdateDTO(ApiClient.CircleJoinRequest circleRequest)
        {
            var circleRequestUpdateDTO = new ApiClient.CircleJoinRequestUpdateDTO
            {
                Id = circleRequest.Id,
                CircleId = circleRequest.CircleId,
                UpdatedByUserId = circleRequest.UpdatedByUserId
            };
            return circleRequestUpdateDTO;
        }

        #endregion
    }
}
