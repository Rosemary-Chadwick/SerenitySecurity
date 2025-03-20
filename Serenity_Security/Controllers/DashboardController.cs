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
    public class DashboardController : ControllerBase
    {
        private readonly Serenity_SecurityDbContext _dbContext;

        public DashboardController(Serenity_SecurityDbContext context)
        {
            _dbContext = context;
        }

        // Helper method to get current user ID
        private async Task<int> GetCurrentUserId()
        {
            // Get the user identity
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(identityUserId))
            {
                throw new Exception("User not found");
            }

            // Get the UserProfile ID from the identity user ID
            var userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(u =>
                u.IdentityUserId == identityUserId
            );

            if (userProfile == null)
            {
                throw new Exception("User profile not found");
            }

            return userProfile.Id;
        }

        [HttpGet("vulnerability-severity")]
        public async Task<IActionResult> GetVulnerabilitySeverityStats()
        {
            try
            {
                var userId = await GetCurrentUserId();

                // Get the user's assets
                var userAssets = await _dbContext
                    .Assets.Where(a => a.UserId == userId)
                    .ToListAsync();

                // Get all reports for these assets
                var assetIds = userAssets.Select(a => a.Id).ToList();
                var reports = await _dbContext
                    .Reports.Where(r => assetIds.Contains(r.AssetId))
                    .Include(r => r.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Vulnerability)
                    .ToListAsync();

                // Get all vulnerabilities from these reports
                var vulnerabilities = reports
                    .SelectMany(r => r.ReportVulnerabilities.Select(rv => rv.Vulnerability))
                    .Where(v => v != null)
                    .ToList();

                // Count by severity level
                var critical = vulnerabilities.Count(v =>
                    (v.SeverityLevel?.ToLower() ?? "") == "critical"
                );
                var high = vulnerabilities.Count(v => (v.SeverityLevel?.ToLower() ?? "") == "high");
                var medium = vulnerabilities.Count(v =>
                    (v.SeverityLevel?.ToLower() ?? "") == "medium"
                    || (v.SeverityLevel?.ToLower() ?? "") == "moderate"
                );
                var low = vulnerabilities.Count(v => (v.SeverityLevel?.ToLower() ?? "") == "low");
                var unknown = vulnerabilities.Count(v =>
                    string.IsNullOrEmpty(v.SeverityLevel)
                    || (
                        v.SeverityLevel.ToLower() != "critical"
                        && v.SeverityLevel.ToLower() != "high"
                        && v.SeverityLevel.ToLower() != "medium"
                        && v.SeverityLevel.ToLower() != "moderate"
                        && v.SeverityLevel.ToLower() != "low"
                    )
                );

                // Return in the format expected by the chart
                var result = new List<object>
                {
                    new { name = "Critical", value = critical },
                    new { name = "High", value = high },
                    new { name = "Medium", value = medium },
                    new { name = "Low", value = low },
                    new { name = "Unknown", value = unknown },
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving vulnerability statistics: {ex.Message}");
            }
        }

        [HttpGet("remediation-status")]
        public async Task<IActionResult> GetRemediationStatusStats()
        {
            try
            {
                var userId = await GetCurrentUserId();

                // Get the user's assets
                var userAssets = await _dbContext
                    .Assets.Where(a => a.UserId == userId)
                    .ToListAsync();

                // Get all reports for these assets
                var assetIds = userAssets.Select(a => a.Id).ToList();
                var reports = await _dbContext
                    .Reports.Where(r => assetIds.Contains(r.AssetId))
                    .Include(r => r.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Vulnerability)
                    .ThenInclude(v => v.RemediationSteps)
                    .ToListAsync();

                // Get all remediations from these vulnerabilities
                var remediations = reports
                    .SelectMany(r => r.ReportVulnerabilities.Select(rv => rv.Vulnerability))
                    .Where(v => v != null)
                    .SelectMany(v => v.RemediationSteps)
                    .Where(rs => rs != null)
                    .ToList();

                var completedCount = remediations.Count(r => r.IsCompleted);
                var pendingCount = remediations.Count - completedCount;

                var result = new List<object>
                {
                    new { name = "Fixed", value = completedCount },
                    new { name = "Pending", value = pendingCount },
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving remediation statistics: {ex.Message}");
            }
        }

        [HttpGet("asset-vulnerabilities")]
        public async Task<IActionResult> GetAssetVulnerabilityStats()
        {
            try
            {
                var userId = await GetCurrentUserId();

                // Get assets for the current user with their reports and vulnerabilities
                var assets = await _dbContext
                    .Assets.Where(a => a.UserId == userId)
                    .Include(a => a.Reports)
                    .ThenInclude(r => r.ReportVulnerabilities)
                    .ThenInclude(rv => rv.Vulnerability)
                    .ToListAsync();

                var result = new List<object>();

                foreach (var asset in assets)
                {
                    // Collect all vulnerabilities for this asset
                    var vulnerabilities = asset
                        .Reports.SelectMany(r =>
                            r.ReportVulnerabilities.Select(rv => rv.Vulnerability)
                        )
                        .Where(v => v != null)
                        .ToList();

                    if (vulnerabilities.Any())
                    {
                        result.Add(
                            new
                            {
                                name = asset.SystemName,
                                critical = vulnerabilities.Count(v =>
                                    (v.SeverityLevel?.ToLower() ?? "") == "critical"
                                ),
                                high = vulnerabilities.Count(v =>
                                    (v.SeverityLevel?.ToLower() ?? "") == "high"
                                ),
                                medium = vulnerabilities.Count(v =>
                                    (v.SeverityLevel?.ToLower() ?? "") == "medium"
                                    || (v.SeverityLevel?.ToLower() ?? "") == "moderate"
                                ),
                                low = vulnerabilities.Count(v =>
                                    (v.SeverityLevel?.ToLower() ?? "") == "low"
                                ),
                                unknown = vulnerabilities.Count(v =>
                                    string.IsNullOrEmpty(v.SeverityLevel)
                                    || (
                                        v.SeverityLevel.ToLower() != "critical"
                                        && v.SeverityLevel.ToLower() != "high"
                                        && v.SeverityLevel.ToLower() != "medium"
                                        && v.SeverityLevel.ToLower() != "moderate"
                                        && v.SeverityLevel.ToLower() != "low"
                                    )
                                ),
                            }
                        );
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    $"Error retrieving asset vulnerability statistics: {ex.Message}"
                );
            }
        }
    }
}
