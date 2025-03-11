using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Data;
using Serenity_Security.Models;
using Serenity_Security.Models.DTOs;
using Serenity_Security.Services;

namespace Serenity_Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VulnerabilityController : ControllerBase
    {
        private readonly Serenity_SecurityDbContext _dbContext;
        private readonly NvdApiService _nvdApiService;

        public VulnerabilityController(
            Serenity_SecurityDbContext context,
            NvdApiService nvdApiService
        )
        {
            _dbContext = context;
            _nvdApiService = nvdApiService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestVulnerabilities()
        {
            var searchResult = await _nvdApiService.SearchVulnerabilitiesByKeywordAsync(
                "ubuntu 18.04"
            );
            var mapped = searchResult
                .Vulnerabilities.Select(v => new
                {
                    id = v.Vulnerability?.Id,
                    description = v
                        .Vulnerability?.Descriptions?.FirstOrDefault(d => d.Lang == "en")
                        ?.Value,
                })
                .ToList();

            return Ok(new { count = mapped.Count, items = mapped.Take(5) });
        }

        [HttpGet("diagnose")]
        public async Task<IActionResult> DiagnoseNvdApi()
        {
            try
            {
                var result = await _nvdApiService.TestApiConnection();
                var sampleCve = await _nvdApiService.SearchVulnerabilitiesByCveIdAsync(
                    "CVE-2021-3156"
                );

                return Ok(
                    new
                    {
                        apiConnected = !string.IsNullOrEmpty(result),
                        vulnsFound = sampleCve.Vulnerabilities?.Count ?? 0,
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVulnerabilityById(int id)
        {
            try
            {
                string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(identityUserId))
                {
                    return Unauthorized();
                }

                UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(up =>
                    up.IdentityUserId == identityUserId
                );

                if (userProfile == null)
                {
                    return Unauthorized();
                }

                Vulnerability vulnerability = await _dbContext
                    .Vulnerabilities.Include(v => v.RemediationSteps) // Make sure to include remediation steps
                    .Include(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .FirstOrDefaultAsync(v =>
                        v.Id == id
                        && v.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (vulnerability == null)
                {
                    return NotFound("Vulnerability not found.");
                }

                VulnerabilityDetailDto vulnerabilityDetail = new VulnerabilityDetailDto
                {
                    Id = vulnerability.Id,
                    CveId = vulnerability.CveId,
                    Description = vulnerability.Description,
                    CvsScore = vulnerability.CvsScore,
                    PublishedAt = vulnerability.PublishedAt,
                    SeverityLevel = vulnerability.SeverityLevel,
                    References = vulnerability.References,
                    ReportId = vulnerability.ReportVulnerabilities.FirstOrDefault()?.ReportId,
                    // Map remediation steps to DTOs
                    RemediationSteps =
                        vulnerability
                            .RemediationSteps?.Select(r => new RemediationChecklistDto
                            {
                                Id = r.Id,
                                VulnerabilityId = r.VulnerabilityId,
                                Description = r.Description,
                                FixedVersion = r.FixedVersion,
                                VerificationSteps = r.VerificationSteps,
                                IsCompleted = r.IsCompleted,
                                CreatedAt = r.CreatedAt,
                            })
                            .ToList() ?? new List<RemediationChecklistDto>(),
                };

                return Ok(vulnerabilityDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestNvdApiConnection()
        {
            try
            {
                var result = await _nvdApiService.TestApiConnection();
                return Ok(new { result = result.Substring(0, Math.Min(1000, result.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("generate-remediation/{id}")]
        public async Task<IActionResult> GenerateRemediationSteps(
            int id,
            [FromQuery] bool forceRegenerate = false
        )
        {
            try
            {
                // Authentication check
                string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(identityUserId))
                {
                    return Unauthorized();
                }

                UserProfile userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(up =>
                    up.IdentityUserId == identityUserId
                );

                if (userProfile == null)
                {
                    return Unauthorized();
                }

                // Get vulnerability with security check
                var vulnerability = await _dbContext
                    .Vulnerabilities.Include(v => v.RemediationSteps)
                    .Include(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .FirstOrDefaultAsync(v =>
                        v.Id == id
                        && v.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (vulnerability == null)
                {
                    return NotFound("Vulnerability not found or you don't have access to it.");
                }

                // Generate remediation steps based on vulnerability type and severity
                var remediationSteps = new List<RemediationChecklist>();

                // If forcing regeneration and steps exist, delete existing steps first
                if (
                    forceRegenerate
                    && vulnerability.RemediationSteps != null
                    && vulnerability.RemediationSteps.Any()
                )
                {
                    _dbContext.RemediationChecklists.RemoveRange(vulnerability.RemediationSteps);
                    await _dbContext.SaveChangesAsync();

                    // Clear the collection after removal
                    vulnerability.RemediationSteps = new List<RemediationChecklist>();
                }

                // Generate new steps if needed
                if (
                    forceRegenerate
                    || vulnerability.RemediationSteps == null
                    || !vulnerability.RemediationSteps.Any()
                )
                {
                    Console.WriteLine(
                        $"Generating new remediation steps for vulnerability {vulnerability.Id}"
                    );

                    // Analyze vulnerability description to determine type
                    string vulnType = DetermineVulnerabilityType(vulnerability.Description);
                    string osType = DetectOsType(vulnerability);

                    // IMPORTANT: Instead of just adding a generic step, add a more detailed one
                    // that includes the vulnerability type in the description
                    remediationSteps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                $"Update affected systems to patch {vulnType} vulnerability {vulnerability.CveId}",
                            VerificationSteps = GetDetailedVerificationSteps(vulnerability, osType),
                            FixedVersion = GetRecommendedVersion(vulnerability, osType),
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );

                    // Add specific steps based on identified vulnerability type
                    AddDetailedRemediationSteps(vulnerability, remediationSteps, vulnType, osType);

                    // Save to database
                    _dbContext.RemediationChecklists.AddRange(remediationSteps);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    remediationSteps = vulnerability.RemediationSteps.ToList();
                }

                // Convert to DTOs with all fields properly populated
                var remediationDtos = remediationSteps
                    .Select(r => new RemediationChecklistDto
                    {
                        Id = r.Id,
                        VulnerabilityId = r.VulnerabilityId,
                        Description = r.Description,
                        FixedVersion = r.FixedVersion,
                        VerificationSteps = r.VerificationSteps,
                        IsCompleted = r.IsCompleted,
                        CreatedAt = r.CreatedAt,
                        VulnerabilityCveId = vulnerability.CveId, // Make sure this is set
                    })
                    .ToList();

                return Ok(remediationDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GenerateRemediationSteps: {ex.Message}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        private string DetermineVulnerabilityType(string description)
        {
            if (string.IsNullOrEmpty(description))
                return "security";

            var desc = description.ToLower();

            if (
                desc.Contains("command injection")
                || desc.Contains("shell")
                || desc.Contains("csrf")
            )
                return "command injection";

            if (
                desc.Contains("memory corruption")
                || desc.Contains("buffer overflow")
                || desc.Contains("denial of service")
            )
                return "memory corruption";

            if (
                desc.Contains("privilege")
                || desc.Contains("permission")
                || desc.Contains("access control")
            )
                return "access control";

            if (desc.Contains("network") || desc.Contains("router") || desc.Contains("firewall"))
                return "network security";

            return "security"; // Default type
        }

        // Enhanced version of AddSpecificRemediationSteps
        private void AddDetailedRemediationSteps(
            Vulnerability vulnerability,
            List<RemediationChecklist> steps,
            string vulnType,
            string osType
        )
        {
            // Add type-specific steps
            switch (vulnType)
            {
                case "command injection":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                "Implement proper input validation to prevent command injection attacks",
                            VerificationSteps =
                                "Review code for input validation and sanitization\nTest with payloads containing shell metacharacters: & | ; $ > <",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                case "memory corruption":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                "Apply vendor patches and implement memory protection controls",
                            VerificationSteps =
                                "Enable memory protection technologies (ASLR, DEP)\nLimit resource allocation to prevent exhaustion",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                case "access control":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description = "Review access controls and permission models",
                            VerificationSteps =
                                "Audit user permissions and roles\nEnsure principle of least privilege is applied\nCheck for proper authentication enforcement",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                case "network security":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                "Implement network-level protections and firmware updates",
                            VerificationSteps =
                                "Update firmware to latest version\nImplement network filtering\nDisable unnecessary services and ports",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                default:
                    // For default/generic vulnerability type
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                "Implement security best practices and vendor recommendations",
                            VerificationSteps =
                                "Follow vendor security guidelines\nImplement defense-in-depth measures\nRegularly scan for vulnerabilities",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;
            }

            // Add OS-specific steps
            AddOsSpecificSteps(vulnerability, steps, osType);
        }

        // Add OS-specific remediation steps
        private void AddOsSpecificSteps(
            Vulnerability vulnerability,
            List<RemediationChecklist> steps,
            string osType
        )
        {
            switch (osType)
            {
                case "ubuntu":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description = "Apply Linux/Ubuntu specific security patches",
                            VerificationSteps =
                                "sudo apt update && sudo apt upgrade\nsudo apt list --upgradable\nRestart affected services",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                case "windows":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description = "Apply Windows security updates and patches",
                            VerificationSteps =
                                "Check Windows Update history\nRun: Get-Hotfix | Where-Object {$_.HotFixID -eq 'KBxxxxxxx'}\nRestart system if required",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;

                case "network":
                    steps.Add(
                        new RemediationChecklist
                        {
                            VulnerabilityId = vulnerability.Id,
                            Description =
                                "Update network device firmware and security configurations",
                            VerificationSteps =
                                "Check device firmware version\nVerify configuration settings\nTest connectivity with security controls in place",
                            IsCompleted = false,
                            CreatedAt = DateTime.UtcNow,
                        }
                    );
                    break;
            }
        }

        // Enhanced detailed verification steps
        private string GetDetailedVerificationSteps(Vulnerability vulnerability, string osType)
        {
            switch (osType)
            {
                case "ubuntu":
                    return "# Ubuntu/Linux verification steps\n"
                        + "sudo apt update && sudo apt upgrade\n"
                        + "sudo apt list --upgradable\n"
                        + "sudo apt install -y [package-name]\n"
                        + "systemctl restart [affected-service]\n"
                        + "# Verify patch is applied\n"
                        + "dpkg -l | grep [package-name]";

                case "windows":
                    return "# Windows verification steps\n"
                        + "Check Windows Update history\n"
                        + "# PowerShell commands\n"
                        + "Get-Hotfix | Where-Object {$_.HotFixID -eq 'KBxxxxxx'}\n"
                        + "# Check affected software version\n"
                        + "[software-name] --version\n"
                        + "# Restart affected services\n"
                        + "Restart-Service -Name [service-name]";

                case "network":
                    return "# Network device verification\n"
                        + "show version\n"
                        + "show running-config\n"
                        + "# Check for security features\n"
                        + "show security\n"
                        + "# Verify connectivity\n"
                        + "ping [test-target]\n"
                        + "traceroute [test-target]";

                default:
                    return "# General verification steps\n"
                        + "1. Update system to latest patches\n"
                        + "2. Verify software versions\n"
                        + "3. Check system configurations\n"
                        + "4. Restart affected services\n"
                        + "5. Test for vulnerability presence";
            }
        }

        // Get recommended version based on OS
        private string GetRecommendedVersion(Vulnerability vulnerability, string osType)
        {
            // In a real application, this might query a database or external service
            // For simplicity, we'll return a placeholder value based on OS

            switch (osType)
            {
                case "ubuntu":
                    return "Ubuntu 20.04 LTS or newer";
                case "windows":
                    return "Windows 10 20H2 or newer with KB5005033";
                case "network":
                    return "Firmware version 12.4.x or newer";
                default:
                    return "Latest vendor-recommended version";
            }
        }

        private string GetVerificationSteps(Vulnerability vulnerability)
        {
            var osType = DetectOsType(vulnerability);

            switch (osType)
            {
                case "ubuntu":
                    return "sudo apt update && sudo apt upgrade\nsudo apt list --upgradable\nRestart affected services";

                case "windows":
                    return "Check Windows Update history\nRun: Get-Hotfix | Where-Object {$_.HotFixID -eq 'KBxxxxxxx'}\nRestart system if required";

                case "network":
                    return "Check device firmware version\nVerify configuration settings\nTest connectivity with security controls in place";

                default:
                    return "Update system to latest patches\nVerify version information\nTest for vulnerability presence";
            }
        }

        private void AddSpecificRemediationSteps(
            Vulnerability vulnerability,
            List<RemediationChecklist> steps
        )
        {
            var desc = vulnerability.Description?.ToLower() ?? "";
            bool patternFound = false;

            // Command injection / Shell issues
            if (
                desc.Contains("command injection")
                || desc.Contains("shell meta")
                || desc.Contains("csrf")
                || desc.Contains("cross-site request forgery")
            )
            {
                steps.Add(
                    new RemediationChecklist
                    {
                        VulnerabilityId = vulnerability.Id,
                        Description =
                            "Implement proper input validation to prevent command injection",
                        VerificationSteps =
                            "Review code for input validation and sanitization\nTest with payloads containing shell metacharacters: & | ; $ > <",
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                    }
                );
                patternFound = true;
                Console.WriteLine("Added: Command injection remediation");
            }

            // Memory corruption issues
            if (
                desc.Contains("memory corruption")
                || desc.Contains("buffer overflow")
                || desc.Contains("resource exhaustion")
                || desc.Contains("denial of service")
            )
            {
                steps.Add(
                    new RemediationChecklist
                    {
                        VulnerabilityId = vulnerability.Id,
                        Description =
                            "Apply vendor patches and implement memory protection controls",
                        VerificationSteps =
                            "Enable memory protection technologies (ASLR, DEP)\nLimit resource allocation to prevent exhaustion",
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                    }
                );
                patternFound = true;
                Console.WriteLine("Added: Memory protection remediation");
            }

            // Access control issues
            if (
                desc.Contains("privilege")
                || desc.Contains("permission")
                || desc.Contains("access control")
                || desc.Contains("escape confinement")
                || desc.Contains("active directory")
            )
            {
                steps.Add(
                    new RemediationChecklist
                    {
                        VulnerabilityId = vulnerability.Id,
                        Description = "Review access controls and permission models",
                        VerificationSteps =
                            "Audit user permissions and roles\nEnsure principle of least privilege is applied\nCheck for proper authentication enforcement",
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                    }
                );
                patternFound = true;
                Console.WriteLine("Added: Access control remediation");
            }

            // Network-specific issues
            if (
                desc.Contains("network")
                || desc.Contains("router")
                || desc.Contains("dd-wrt")
                || desc.Contains("remote")
                || desc.Contains("website")
            )
            {
                steps.Add(
                    new RemediationChecklist
                    {
                        VulnerabilityId = vulnerability.Id,
                        Description = "Implement network-level protections and firmware updates",
                        VerificationSteps =
                            "Update firmware to latest version\nImplement network filtering\nDisable unnecessary services and ports",
                        IsCompleted = false,
                        CreatedAt = DateTime.UtcNow,
                    }
                );
                patternFound = true;
                Console.WriteLine("Added: Network security remediation");
            }

            // If no specific pattern was found, add a generic step
            if (!patternFound)
            {
                Console.WriteLine("No specific patterns detected, using generic remediation");
            }
        }

        private string DetectOsType(Vulnerability vulnerability)
        {
            var desc = vulnerability.Description?.ToLower() ?? "";
            var cveId = vulnerability.CveId?.ToLower() ?? "";

            Console.WriteLine($"Analyzing vulnerability: {vulnerability.CveId}");
            Console.WriteLine(
                $"Description excerpt: {desc.Substring(0, Math.Min(100, desc.Length))}"
            );

            // Check for specific OS mentions
            if (
                desc.Contains("ubuntu")
                || desc.Contains("linux")
                || desc.Contains("debian")
                || desc.Contains("cups")
                || desc.Contains("apparmor")
                || desc.Contains("nouveau")
            )
            {
                Console.WriteLine("Detected: Ubuntu/Linux");
                return "ubuntu";
            }

            if (
                desc.Contains("windows nt")
                || desc.Contains("windows 2000")
                || desc.Contains("windows server")
                || desc.Contains("microsoft")
                || desc.Contains("active directory")
            )
            {
                Console.WriteLine("Detected: Windows");
                return "windows";
            }

            if (
                desc.Contains("dd-wrt")
                || desc.Contains("router")
                || desc.Contains("firewall")
                || desc.Contains("httpd")
                || desc.Contains("network device")
            )
            {
                Console.WriteLine("Detected: Network device");
                return "network";
            }

            Console.WriteLine("No specific OS detected, using generic");
            return "generic";
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVulnerabilities([FromQuery] string keyword)
        {
            try
            {
                string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(identityUserId))
                {
                    return Unauthorized();
                }

                UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(up =>
                    up.IdentityUserId == identityUserId
                );

                if (userProfile == null)
                {
                    return Unauthorized();
                }

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return BadRequest("Keyword is required");
                }

                var nvdResponse = await _nvdApiService.SearchVulnerabilitiesByKeywordAsync(keyword);
                var vulnerabilities = new List<VulnerabilityListDto>();

                foreach (var vuln in nvdResponse.Vulnerabilities)
                {
                    try
                    {
                        if (vuln.Vulnerability == null)
                            continue;

                        // Get description - prefer English
                        var description = "No description available";
                        if (vuln.Vulnerability.Descriptions?.Any() == true)
                        {
                            var descEntry = vuln.Vulnerability.Descriptions.FirstOrDefault(d =>
                                d.Lang == "en"
                            );
                            if (descEntry != null)
                            {
                                description = descEntry.Value;
                            }
                        }

                        // Get CVSS score if available
                        decimal cvssScore = 0;
                        string severity = "Unknown";

                        if (vuln.Vulnerability.Metrics?.CvssMetricV31?.Any() == true)
                        {
                            var cvssData = vuln
                                .Vulnerability.Metrics.CvssMetricV31.FirstOrDefault()
                                ?.CvssData;
                            if (cvssData != null)
                            {
                                cvssScore = cvssData.BaseScore;
                                severity = cvssData.BaseSeverity;
                            }
                        }

                        DateTime? publishedDate = null;
                        if (!string.IsNullOrEmpty(vuln.Vulnerability.Published))
                        {
                            try
                            {
                                publishedDate = DateTime.Parse(vuln.Vulnerability.Published);
                            }
                            catch
                            {
                                // Use null if date can't be parsed
                            }
                        }

                        // Create DTO
                        vulnerabilities.Add(
                            new VulnerabilityListDto
                            {
                                CveId = vuln.Vulnerability.Id,
                                Description = description,
                                CvsScore = cvssScore,
                                SeverityLevel = severity,
                                PublishedAt = publishedDate,
                            }
                        );
                    }
                    catch
                    {
                        // Silently continue processing other vulnerabilities
                    }
                }

                return Ok(vulnerabilities);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while searching for vulnerabilities");
            }
        }

        [HttpGet("scan/{assetId}")]
        public async Task<IActionResult> ScanAssetForVulnerabilities(int assetId)
        {
            try
            {
                // Authentication check
                string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(identityUserId))
                {
                    return Unauthorized();
                }

                UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(up =>
                    up.IdentityUserId == identityUserId
                );

                if (userProfile == null)
                {
                    return Unauthorized();
                }

                // Get asset
                var asset = await _dbContext
                    .Assets.Include(a => a.SystemType)
                    .FirstOrDefaultAsync(a => a.Id == assetId && a.UserId == userProfile.Id);

                if (asset == null)
                {
                    return NotFound("Asset not found or you don't have access to it.");
                }

                // Create a new report first
                var report = new Report
                {
                    AssetId = assetId,
                    CreatedAt = DateTime.UtcNow,
                    IsCompleted = false,
                    ReportVulnerabilities = new List<ReportVulnerability>(),
                };

                _dbContext.Reports.Add(report);
                await _dbContext.SaveChangesAsync();

                // Search for real vulnerabilities using the NVD API
                // Replace the search term generation with this:
                string searchTerm;
                var osLower = asset.OsVersion.ToLower();

                // Generate appropriate search term based on OS
                if (osLower.Contains("ubuntu"))
                {
                    searchTerm = "ubuntu vulnerability";
                    if (osLower.Contains("18.04"))
                        searchTerm = "ubuntu 18.04 cve";
                }
                else if (osLower.Contains("dd-wrt") || osLower.Contains("ddwrt"))
                {
                    searchTerm = "dd-wrt vulnerability";
                }
                else if (osLower.Contains("windows"))
                {
                    searchTerm = "windows vulnerability";
                }
                else
                {
                    // Extract the first word of the OS as the key term
                    string firstWord = asset.OsVersion.Split(' ')[0];
                    searchTerm = $"{firstWord} vulnerability";
                }

                Console.WriteLine($"Using search term: {searchTerm}");
                var nvdResponse = await _nvdApiService.SearchVulnerabilitiesByKeywordAsync(
                    searchTerm
                );

                // If no results found, try a more generic term
                if ((nvdResponse?.Vulnerabilities?.Count ?? 0) == 0)
                {
                    string fallbackTerm = "common critical vulnerability";
                    Console.WriteLine($"No results with first term. Trying: {fallbackTerm}");
                    nvdResponse = await _nvdApiService.SearchVulnerabilitiesByKeywordAsync(
                        fallbackTerm
                    );
                }

                Console.WriteLine(
                    $"Found {nvdResponse.Vulnerabilities?.Count ?? 0} vulnerabilities in API response"
                );

                // Process real vulnerabilities from API
                int processedCount = 0;
                foreach (var nvdVuln in nvdResponse.Vulnerabilities)
                {
                    try
                    {
                        var cveDetails = nvdVuln.Vulnerability;
                        if (cveDetails == null)
                        {
                            continue;
                        }

                        // Get description from the first English entry
                        var description = "No description available";
                        if (cveDetails.Descriptions != null && cveDetails.Descriptions.Any())
                        {
                            var descEntry = cveDetails.Descriptions.FirstOrDefault(d =>
                                d.Lang == "en"
                            );
                            if (descEntry != null)
                            {
                                description = descEntry.Value;
                            }
                        }

                        // Get CVSS score if available
                        decimal cvssScore = 0;
                        string severity = "Unknown";

                        if (
                            cveDetails.Metrics?.CvssMetricV31 != null
                            && cveDetails.Metrics.CvssMetricV31.Any()
                        )
                        {
                            var cvssData = cveDetails
                                .Metrics.CvssMetricV31.FirstOrDefault()
                                ?.CvssData;
                            if (cvssData != null)
                            {
                                cvssScore = cvssData.BaseScore;
                                severity = cvssData.BaseSeverity;
                            }
                        }

                        DateTime? publishedDate = null;
                        if (!string.IsNullOrEmpty(cveDetails.Published))
                        {
                            try
                            {
                                publishedDate = DateTime.Parse(cveDetails.Published);
                            }
                            catch
                            {
                                // Use null if date can't be parsed
                            }
                        }

                        // References
                        string references = string.Empty;
                        if (cveDetails.References != null && cveDetails.References.Any())
                        {
                            references = string.Join("|", cveDetails.References.Select(r => r.Url));
                        }

                        // Check if vulnerability already exists in our database
                        var existingVuln = await _dbContext.Vulnerabilities.FirstOrDefaultAsync(v =>
                            v.CveId == cveDetails.Id
                        );

                        Vulnerability vulnerability;
                        if (existingVuln == null)
                        {
                            // Create new vulnerability
                            vulnerability = new Vulnerability
                            {
                                CveId = cveDetails.Id,
                                Description = description,
                                CvsScore = cvssScore,
                                SeverityLevel = severity,
                                PublishedAt = publishedDate,
                                References = references,
                                RemediationSteps = new List<RemediationChecklist>(),
                            };

                            _dbContext.Vulnerabilities.Add(vulnerability);
                            await _dbContext.SaveChangesAsync();

                            // Analyze vulnerability to determine type and OS
                            string vulnType = DetermineVulnerabilityType(vulnerability.Description);
                            string osType = DetectOsType(vulnerability);

                            // Create a list to hold our remediation steps
                            var remediationSteps = new List<RemediationChecklist>();

                            // Add the primary remediation step
                            remediationSteps.Add(
                                new RemediationChecklist
                                {
                                    VulnerabilityId = vulnerability.Id,
                                    Description =
                                        $"Update affected systems to patch {vulnType} vulnerability {vulnerability.CveId}",
                                    VerificationSteps = GetDetailedVerificationSteps(
                                        vulnerability,
                                        osType
                                    ),
                                    FixedVersion = GetRecommendedVersion(vulnerability, osType),
                                    IsCompleted = false,
                                    CreatedAt = DateTime.UtcNow,
                                }
                            );

                            // Add type-specific and OS-specific steps
                            AddDetailedRemediationSteps(
                                vulnerability,
                                remediationSteps,
                                vulnType,
                                osType
                            );

                            // Add all steps to the database
                            _dbContext.RemediationChecklists.AddRange(remediationSteps);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            vulnerability = existingVuln;
                        }

                        // Check if this vulnerability is already associated with the report
                        var existingReportVuln =
                            await _dbContext.ReportVulnerabilities.FirstOrDefaultAsync(rv =>
                                rv.ReportId == report.Id && rv.VulnerabilityId == vulnerability.Id
                            );

                        if (existingReportVuln == null)
                        {
                            // Add to report
                            var reportVulnerability = new ReportVulnerability
                            {
                                ReportId = report.Id,
                                VulnerabilityId = vulnerability.Id,
                                DiscoveredAt = DateTime.UtcNow,
                            };

                            _dbContext.ReportVulnerabilities.Add(reportVulnerability);
                            processedCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Silently continue to next vulnerability
                    }
                }

                await _dbContext.SaveChangesAsync();
                Console.WriteLine(
                    $"Successfully processed {processedCount} vulnerabilities from API"
                );

                // Return the report ID
                return Ok(new { reportId = report.Id, vulnerabilitiesCount = processedCount });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
