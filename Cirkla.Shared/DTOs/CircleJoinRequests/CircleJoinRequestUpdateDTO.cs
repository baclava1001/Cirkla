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
    public class CircleJoinRequestUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CircleId { get; set; }
        [Required]
        public string UpdatedByUserId { get; set; }
    }
}
