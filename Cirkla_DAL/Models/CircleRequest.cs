using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_DAL.Models.Enums;

namespace Cirkla_DAL.Models
{
    public class CircleRequest
    {
        [Required]
        public int Id { get; set; }
        [Required, ForeignKey("CircleId")]
        public Circle Circle { get; set; }
        [Required, ForeignKey("FromUserId")]
        public User FromUser { get; set; }
        [Required]
        public CircleJoinRequestType RequestType { get; set; }
        [Required]
        public CircleRequestStatus Status { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        [ForeignKey("UpdatedByUserId")]
        public User? UpdatedByUser { get; set; }
    }
}