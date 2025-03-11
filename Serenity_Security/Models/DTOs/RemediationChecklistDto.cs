namespace Serenity_Security.Models.DTOs;

public class RemediationChecklistDto
{
    public int Id { get; set; }
    public int VulnerabilityId { get; set; }
    public string Description { get; set; }
    public string FixedVersion { get; set; }
    public string VerificationSteps { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public string VulnerabilityCveId { get; set; }
}

public class RemediationUpdateDto
{
    public bool IsCompleted { get; set; }
}

public class RemediationCreateDto
{
    public int VulnerabilityId { get; set; }
    public string Description { get; set; }
    public string FixedVersion { get; set; }
    public string VerificationSteps { get; set; }
}
