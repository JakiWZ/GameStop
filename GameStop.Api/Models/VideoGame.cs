using System.ComponentModel.DataAnnotations;

namespace GameStop.Api.Models;

public class VideoGame
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public string Developer { get; set; } = string.Empty;

    public int CopiesCount { get; set; }

    public decimal Price { get; set; }
}
