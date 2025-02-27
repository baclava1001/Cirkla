using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models;

namespace Mapping.DTOs.ContractNotifications;

public class ContractNotificationForViews
{
    [Required]
    public int Id { get; set; }
    public string? NotificationMessage { get; set; } // "(User X) (requested/replied/picked up/returned/cancelled) (borrowing of Item Y) (on date Z)" 
    [Required]
    public DateTime CreatedAt { get; set; }
    public bool HasBeenRead { get; set; }

    // From Contract properties
    [Required]
    public int ContractId { get; set; }
    [Required]
    public string ItemName { get; set; }
    [Required]
    public string OwnerFullName { get; set; }
    [Required]
    public string BorrowerFullName { get; set; }
    [Required]
    public DateTime Created { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}