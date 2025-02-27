namespace Serenity_Security.Models.DTOs;

public class RemediationChecklistDto
{
    public int Id { get; set; }
    public string FixedVersion { get; set; }
    public string VerificationSteps { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public VulnerabilityListDto Vulnerability { get; set; }
}
