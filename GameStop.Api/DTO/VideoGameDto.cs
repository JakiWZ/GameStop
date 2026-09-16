namespace GameStop.Api.DTO;

public class VideoGameDto
{
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public int CopiesCount { get; set; }
    public decimal Price { get; set; }
}
