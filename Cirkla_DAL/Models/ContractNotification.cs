using System.ComponentModel.DataAnnotations;

namespace Cirkla_DAL.Models;

public class ContractNotification
{ 
    [Required]
    public int Id { get; set; }
    public string? NotificationMessage { get; set; }
    [Required]
    public Contract Contract { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasBeenRead { get; set; }
}