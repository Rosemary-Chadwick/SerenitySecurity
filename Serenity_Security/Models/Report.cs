namespace Serenity_Security.Models;

public class Report
{
    public int Id { get; set; }
    public int AssetId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Asset Asset { get; set; }
    public List<ReportVulnerability> ReportVulnerabilities { get; set; }
}
