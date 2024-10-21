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
        [ForeignKey("Item"), Required, DeleteBehavior(DeleteBehavior.NoAction)]
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        [ForeignKey("Owner"), Required, DeleteBehavior(DeleteBehavior.NoAction)]
        public string OwnerId { get; set; }
        public User Owner { get; set; }
        [ForeignKey("Borrower"), Required, DeleteBehavior(DeleteBehavior.NoAction)]
        public string BorrowerId { get; set; }
        public User Borrower { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
        [Required]
        public DateTimeOffset EndTime { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;

        // TODO: Later add more timestamps for: AgreedStartTime, AgreedEndTime & ActualStartTime, ActualEndTime
    }
}
