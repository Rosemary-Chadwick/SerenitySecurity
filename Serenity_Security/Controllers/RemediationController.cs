using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Data;
using Serenity_Security.Models;
using Serenity_Security.Models.DTOs;

namespace Serenity_Security.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RemediationController : ControllerBase
    {
        private readonly Serenity_SecurityDbContext _dbContext;

        public RemediationController(Serenity_SecurityDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemediationById(int id)
        {
            try
            {
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

                // Get remediation with security check (ensure the user has access to this vulnerability)
                var remediation = await _dbContext
                    .RemediationChecklists.Include(r => r.Vulnerability)
                    .ThenInclude(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .FirstOrDefaultAsync(r =>
                        r.Id == id
                        && r.Vulnerability.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (remediation == null)
                {
                    return NotFound("Remediation item not found or you don't have access to it.");
                }

                // Convert to DTO
                var remediationDto = new RemediationChecklistDto
                {
                    Id = remediation.Id,
                    VulnerabilityId = remediation.VulnerabilityId,
                    Description = remediation.Description,
                    FixedVersion = remediation.FixedVersion,
                    VerificationSteps = remediation.VerificationSteps,
                    IsCompleted = remediation.IsCompleted,
                    CreatedAt = remediation.CreatedAt,
                    VulnerabilityCveId = remediation.Vulnerability.CveId,
                };

                return Ok(remediationDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("vulnerability/{vulnerabilityId}")]
        public async Task<IActionResult> GetRemediationsByVulnerabilityId(int vulnerabilityId)
        {
            try
            {
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

                // Security check - ensure the user has access to this vulnerability
                var hasAccess = await _dbContext
                    .Vulnerabilities.Include(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .AnyAsync(v =>
                        v.Id == vulnerabilityId
                        && v.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (!hasAccess)
                {
                    return Unauthorized("You don't have access to this vulnerability.");
                }

                var remediations = await _dbContext
                    .RemediationChecklists.Where(r => r.VulnerabilityId == vulnerabilityId)
                    .Select(r => new RemediationChecklistDto
                    {
                        Id = r.Id,
                        VulnerabilityId = r.VulnerabilityId,
                        Description = r.Description,
                        FixedVersion = r.FixedVersion,
                        VerificationSteps = r.VerificationSteps,
                        IsCompleted = r.IsCompleted,
                        CreatedAt = r.CreatedAt,
                    })
                    .ToListAsync();

                return Ok(remediations);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleRemediationStatus(
            int id,
            [FromBody] RemediationUpdateDto updateDto
        )
        {
            try
            {
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

                // Get remediation with security check
                var remediation = await _dbContext
                    .RemediationChecklists.Include(r => r.Vulnerability)
                    .ThenInclude(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .FirstOrDefaultAsync(r =>
                        r.Id == id
                        && r.Vulnerability.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (remediation == null)
                {
                    return NotFound("Remediation item not found or you don't have access to it.");
                }

                // Update completion status
                remediation.IsCompleted = updateDto.IsCompleted;
                await _dbContext.SaveChangesAsync();

                // Return updated item
                var remediationDto = new RemediationChecklistDto
                {
                    Id = remediation.Id,
                    VulnerabilityId = remediation.VulnerabilityId,
                    Description = remediation.Description,
                    FixedVersion = remediation.FixedVersion,
                    VerificationSteps = remediation.VerificationSteps,
                    IsCompleted = remediation.IsCompleted,
                    CreatedAt = remediation.CreatedAt,
                };

                return Ok(remediationDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRemediation(
            [FromBody] RemediationCreateDto createDto
        )
        {
            try
            {
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

                // Security check - ensure the user has access to this vulnerability
                var hasAccess = await _dbContext
                    .Vulnerabilities.Include(v => v.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Report)
                    .ThenInclude(r => r.Asset)
                    .AnyAsync(v =>
                        v.Id == createDto.VulnerabilityId
                        && v.ReportVulnerabilities.Any(rv =>
                            rv.Report.Asset.UserId == userProfile.Id
                        )
                    );

                if (!hasAccess)
                {
                    return Unauthorized("You don't have access to this vulnerability.");
                }

                // Create new remediation item
                var remediation = new RemediationChecklist
                {
                    VulnerabilityId = createDto.VulnerabilityId,
                    Description = createDto.Description,
                    FixedVersion = createDto.FixedVersion,
                    VerificationSteps = createDto.VerificationSteps,
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                };

                _dbContext.RemediationChecklists.Add(remediation);
                await _dbContext.SaveChangesAsync();

                // Return created item
                var remediationDto = new RemediationChecklistDto
                {
                    Id = remediation.Id,
                    VulnerabilityId = remediation.VulnerabilityId,
                    Description = remediation.Description,
                    FixedVersion = remediation.FixedVersion,
                    VerificationSteps = remediation.VerificationSteps,
                    IsCompleted = remediation.IsCompleted,
                    CreatedAt = remediation.CreatedAt,
                };

                return CreatedAtAction(
                    nameof(GetRemediationById),
                    new { id = remediation.Id },
                    remediationDto
                );
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
