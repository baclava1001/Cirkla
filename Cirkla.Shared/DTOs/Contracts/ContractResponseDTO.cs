using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_DAL.Models;

namespace Cirkla.Shared.DTOs.Contracts
{
    public class ContractResponseDTO
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemModel { get; set; }
        public string ItemSpecifications { get; set; }
        public List<ItemPicture>? ItemPictures { get; set; }

        public string OwnerId { get; set; }
        public string OwnerFullName { get; set; }
        public string OwnerAddress { get; set; }
        public string OwnerZipCode { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string? OwnerProfilePictureUrl { get; set; }

        public string BorrowerId { get; set; }
        public string BorrowerFullName { get; set; }
        public string BorrowerAddress { get; set; }
        public string BorrowerZipCode { get; set; }
        public string BorrowerEmail { get; set; }
        public string BorrowerPhoneNumber { get; set; }
        public string? BorrowerProfilePictureUrl { get; set; }

        public DateTime Created { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ContractStatusChange>? StatusChanges { get; set; }
    }
}
