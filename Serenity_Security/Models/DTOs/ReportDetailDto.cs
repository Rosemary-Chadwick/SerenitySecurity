namespace Serenity_Security.Models.DTOs;

public class ReportDetailsDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public AssetDto Asset { get; set; }
    public List<VulnerabilityDetailDto> Vulnerabilities { get; set; }
}
