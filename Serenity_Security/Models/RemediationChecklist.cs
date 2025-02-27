namespace Serenity_Security.Models;

public class RemediationChecklist
{
    public int Id { get; set; }
    public string FixedVersion { get; set; }
    public string VerificationSteps { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public int VulnerabilityId { get; set; }
    public Vulnerability Vulnerability { get; set; }
}
