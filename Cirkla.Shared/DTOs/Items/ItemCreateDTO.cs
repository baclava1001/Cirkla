using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models;

namespace Cirkla.Shared.DTOs.Items;

public class ItemCreateDTO
{
    [Required]
    public string Name { get; set; }
    public string? Category { get; set; }
    public string? Model { get; set; }
    public string? Specifications { get; set; }
    public string? Description { get; set; }
    [Required]
    public string OwnerId { get; set; }
    public List<ItemPicture>? Pictures { get; set; }
}