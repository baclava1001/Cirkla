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
    public class CircleJoinRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public CircleJoinRequestType Type { get; set; }

        [Required]
        public int CircleId { get; set; }
        public Circle Circle { get; set; }

        [Required]
        public string TargetUserId { get; set; }
        public User TargetUser { get; set; }
        
        [Required]
        public string FromUserId { get; set; }
        public User FromUser { get; set; }
        
        public string? UpdatedByUserId { get; set; }
        public User? UpdatedByUser { get; set; }
        
        [Required]
        public CircleRequestStatus Status { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
        
        [Required]
        public DateTime? ExpiresAt { get; set; }
    }
}