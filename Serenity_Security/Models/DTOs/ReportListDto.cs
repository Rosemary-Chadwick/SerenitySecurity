namespace Serenity_Security.Models.DTOs;

public class ReportListDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AssetName { get; set; }
    public int VulnerabilityCount { get; set; }
    public int HighSeverityCount { get; set; }
}
