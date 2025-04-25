using System.ComponentModel.DataAnnotations;
using Cirkla_DAL.Models;

namespace Cirkla.Shared.DTOs.Circles;

public class CircleCreateDTO
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public List<User> Administrators { get; set; } // All administrators are also members

    public List<User> Members { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedById { get; set; }
}