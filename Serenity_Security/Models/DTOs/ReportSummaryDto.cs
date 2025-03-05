namespace Serenity_Security.Models.DTOs;

public class ReportSummaryDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public int AssetId { get; set; }
    public AssetDto Asset { get; set; }
}
