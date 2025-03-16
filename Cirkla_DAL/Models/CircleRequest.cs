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
        [Required]
        public int CircleId { get; set; }
        public Circle Circle { get; set; }
        [Required]
        public string PendingMemberId { get; set; }
        public User PendingMember { get; set; }
        [Required]
        public string FromUserId { get; set; }
        public User FromUser { get; set; }
        public string? UpdatedByUserId { get; set; }
        public User? UpdatedByUser { get; set; }
        [Required]
        public CircleJoinRequestType RequestType { get; set; }
        [Required]
        public CircleRequestStatus Status { get; set; }
        [Required]
        public DateTime RequestDate { get; set; }
        public DateTime? UpdatedAt { get; set; }
        // TODO: Make ExpiresAt required
        public DateTime? ExpiresAt { get; set; }
    }
}