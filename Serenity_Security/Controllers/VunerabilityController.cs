using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Data;
using Serenity_Security.Models;
using Serenity_Security.Models.DTOs;

namespace Serenity_Security.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VulnerabilityController : ControllerBase
{
    private Serenity_SecurityDbContext _dbContext;

    public VulnerabilityController(Serenity_SecurityDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetVulnerabilityById(int id)
    {
        try
        { // find the current user
            string identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(identityUserId))
            {
                return Unauthorized();
            }
            // find that users info
            UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(up =>
                up.IdentityUserId == identityUserId
            );

            if (userProfile == null)
            {
                return Unauthorized();
            }

            // Check if the vulnerability exists and if the user has access to it
            // by checking if it's part of a report for one of their assets
            // User → Asset → Report → ReportVulnerability → Vulnerability : is an ownership verification path
            Vulnerability vulnerability = _dbContext
                .Vulnerabilities.Include(v => v.ReportVulnerabilities)
                .ThenInclude(rv => rv.Report)
                .ThenInclude(r => r.Asset)
                .FirstOrDefault(v =>
                    v.Id == id
                    && v.ReportVulnerabilities.Any(rv => rv.Report.Asset.UserId == userProfile.Id)
                );
            // v.ReportVulnerabilities: This gives us the collection of all report-vulnerability associations for this vulnerability
            // .Any(): This checks if at least one item in the collection satisfies the condition
            // rv => rv.Report.Asset.UserId == userProfile.Id: This is the condition - following the chain from report-vulnerability → report → asset → user to check if the asset belongs to the current user
            //

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
            };

            return Ok(vulnerabilityDetail);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
