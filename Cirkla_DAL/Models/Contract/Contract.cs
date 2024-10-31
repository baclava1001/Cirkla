using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cirkla_DAL.Models.Items;
using Cirkla_DAL.Models.Users;
using Microsoft.EntityFrameworkCore;


namespace Cirkla_DAL.Models.Contract
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
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? AcceptedByOwner { get; set; }
        public DateTime? DeniedByOwner { get; set; }

        // TODO: Later add more timestamps for: AgreedStartTime, AgreedEndTime & ActualStartTime, ActualEndTime
    }
}
