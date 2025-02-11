using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Cirkla_DAL.Models.Enums;


namespace Cirkla_DAL.Models
{
    public class Contract
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Item Item { get; set; }
        [Required]
        public User Owner { get; set; }
        [Required]
        public User Borrower { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public DateTime? AcceptedByOwner { get; set; }
        public DateTime? DeniedByOwner { get; set; }
        //public DateTime? CancelledByOwner { get; set; }
        //public DateTime? AcceptedByBorrower { get; set; }
        //public DateTime? CancelledByBorrower { get; set; }
        //public DateTime? PickedUpTime { get; set; }
        //public DateTime? ReturnedTime { get; set; }
        //public ContractStatus Status { get; set; }

    }
}