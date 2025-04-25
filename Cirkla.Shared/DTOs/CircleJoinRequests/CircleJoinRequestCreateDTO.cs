using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;

namespace Cirkla.Shared.DTOs.CircleJoinRequests
{
    public class CircleJoinRequestCreateDTO
    {
        [Required]
        public int CircleId { get; set; }
        [Required]
        public CircleJoinRequestType Type { get; set; }
        [Required]
        public string TargetUserId { get; set; }
        [Required]
        public string FromUserId { get; set; }
        public string? UpdatedByUserId { get; set; }
        [Required]
        public CircleRequestStatus Status { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        [Required]
        public DateTime? ExpiresAt { get; set; }
    }
}
