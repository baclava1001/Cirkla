using System.Net.NetworkInformation;
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

        public static async Task<Item> MapToItem(ItemCreateDTO itemCreateDTO)
        {
            return new Item
            {
                Name = itemCreateDTO.Name,
                Category = itemCreateDTO.Category,
                Model = itemCreateDTO.Model,
                Specifications = itemCreateDTO.Specifications,
                Description = itemCreateDTO.Description,
                OwnerId = itemCreateDTO.OwnerId,
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
        public static async Task<Contract> MapToContract(ContractCreateDTO contractCreateDTO)
        {
            var contract = new Contract
            {
                ItemId = contractCreateDTO.ItemId,
                OwnerId = contractCreateDTO.OwnerId,
                BorrowerId = contractCreateDTO.BorrowerId,
                Created = contractCreateDTO.Created,
                StartTime = contractCreateDTO.StartTime,
                EndTime = contractCreateDTO.EndTime,
                StatusChanges = new List<ContractStatusChange>()
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


        public static async Task<Contract> MapToContract(ContractUpdateDTO contractUpdateDTO)
        {
            var contract = new Contract()
            {
                Id = contractUpdateDTO.Id,
                ItemId = contractUpdateDTO.ItemId,
                OwnerId = contractUpdateDTO.OwnerId,
                BorrowerId = contractUpdateDTO.BorrowerId,
                Created = contractUpdateDTO.Created,
                StartTime = contractUpdateDTO.StartTime,
                EndTime = contractUpdateDTO.EndTime,
                StatusChanges = new List<ContractStatusChange>
                {
                    new ContractStatusChange
                    {
                        ChangedAt = contractUpdateDTO.UpdatedAt,
                        ChangedById = contractUpdateDTO.UpdatedByUserId,
                        From = contractUpdateDTO.FromStatus,
                        To = contractUpdateDTO.ToStatus,
                    }
                }
            };
            return contract;
        }


        public static async Task<ContractUpdateDTO> MapToContractUpdateDTO(Contract contract)
        {
            var contractUpdateDTO = new ContractUpdateDTO()
            {
                Id = contract.Id,
                ItemId = contract.Item.Id,
                OwnerId = contract.Owner.Id,
                BorrowerId = contract.Borrower.Id,
                Created = contract.Created,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                UpdatedByUserId = contract.StatusChanges.Last().ChangedBy.Id,
                UpdatedAt = contract.StatusChanges.Last().ChangedAt,
                FromStatus = contract.StatusChanges.Last().From,
                ToStatus = contract.StatusChanges.Last().To
            };
            return contractUpdateDTO;
        }


        public static async Task<ContractResponseDTO> MapToContractResponseDTO(Contract contract)
        {
            var contractResponseDTO = new ContractResponseDTO
            {
                Id = contract.Id,

                ItemId = contract.Item.Id,
                ItemName = contract.Item.Name,
                ItemDescription = contract.Item.Description,
                ItemModel = contract.Item.Model,
                ItemSpecifications = contract.Item.Specifications,
                ItemPictures = contract.Item.Pictures,

                OwnerId = contract.Owner.Id,
                OwnerFullName = contract.Owner.FullName,
                OwnerAddress = contract.Owner.Address,
                OwnerZipCode = contract.Owner.ZipCode,
                OwnerEmail = contract.Owner.Email,
                OwnerPhoneNumber = contract.Owner.PhoneNumber,
                OwnerProfilePictureUrl = contract.Owner.ProfilePictureURL,

                BorrowerId = contract.Borrower.Id,
                BorrowerFullName = contract.Borrower.FullName,
                BorrowerAddress = contract.Borrower.Address,
                BorrowerZipCode = contract.Borrower.ZipCode,
                BorrowerEmail = contract.Borrower.Email,
                BorrowerPhoneNumber = contract.Borrower.PhoneNumber,
                BorrowerProfilePictureUrl = contract.Borrower.ProfilePictureURL,

                Created = contract.Created,
                StartTime = contract.StartTime,
                EndTime = contract.EndTime,
                StatusChanges = contract.StatusChanges
            };
            return contractResponseDTO;
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
                ContractId = contractNotification.ContractId,
                ItemName = contractNotification.Contract.Item.Name,
                OwnerFullName = contractNotification.Contract.Owner.FullName,
                BorrowerFullName = contractNotification.Contract.Borrower.FullName,
                Created = contractNotification.Contract.Created,
                StartTime = contractNotification.Contract.StartTime,
                EndTime = contractNotification.Contract.EndTime
            };
            return contractNotificationForViews;
        }


        public static async Task<ContractNotification> MapToContractNotification(ContractNotificationForViews contractNotificationForViews)
        {
            var contractNotification = new ContractNotification
            {
                Id = contractNotificationForViews.Id,
                NotificationMessage = contractNotificationForViews.NotificationMessage,
                CreatedAt = contractNotificationForViews.CreatedAt,
                HasBeenRead = contractNotificationForViews.HasBeenRead,
                ContractId = contractNotificationForViews.ContractId
            };
            return contractNotification;
        }

        #endregion

        #region CircleRequests

        public static async Task<CircleJoinRequest> MapToCircleRequest(CircleJoinRequestCreateDTO circleJoinRequestCreateDto)
        {
            var circleRequest = new CircleJoinRequest
            {
                CircleId = circleJoinRequestCreateDto.CircleId,
                Type = circleJoinRequestCreateDto.Type,
                TargetUserId = circleJoinRequestCreateDto.TargetUserId,
                FromUserId = circleJoinRequestCreateDto.FromUserId,
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
