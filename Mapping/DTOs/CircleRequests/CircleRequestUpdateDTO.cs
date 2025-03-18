using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirkla_DAL.Models;
using Cirkla_DAL.Models.Enums;

namespace Mapping.DTOs.CircleRequests
{
    public class CircleRequestUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CircleId { get; set; }
        [Required]
        public string PendingMemberId { get; set; }
        [Required]
        public string FromUserId { get; set; }
        public string? UpdatedByUserId { get; set; }
        [Required]
        public CircleJoinRequestType RequestType { get; set; }
        [Required]
        public CircleRequestStatus Status { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public DateTime? ExpiresAt { get; set; }
    }
}
