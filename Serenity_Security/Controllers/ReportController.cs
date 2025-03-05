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
public class ReportController : ControllerBase
{
    private Serenity_SecurityDbContext _dbContext;

    public ReportController(Serenity_SecurityDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet("asset/{assetId}")]
    public IActionResult GetReportsByAssetId(int assetId)
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
            // find the asset's info
            Asset asset = _dbContext.Assets.FirstOrDefault(a =>
                a.Id == assetId && a.UserId == userProfile.Id
            );

            if (asset == null)
            {
                return NotFound("Asset not found.");
            }

            List<ReportSummaryDto> reports = _dbContext
                .Reports.Where(r => r.AssetId == assetId)
                .Select(r => new ReportSummaryDto
                {
                    Id = r.Id,
                    AssetId = r.AssetId,
                    CreatedAt = r.CreatedAt,
                })
                .OrderByDescending(r => r.CreatedAt)
                .ToList();

            return Ok(reports);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetReportById(int id)
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

            Report report = _dbContext
                .Reports.Include(r => r.Asset)
                .Include(r => r.ReportVulnerabilities)
                .ThenInclude(rv => rv.Vulnerability)
                .FirstOrDefault(r => r.Id == id && r.Asset.UserId == userProfile.Id);

            if (report == null)
            {
                return NotFound("Report not found");
            }

            AssetDto assetDto = new AssetDto
            {
                Id = report.Asset.Id,
                SystemName = report.Asset.SystemName,
                IpAddress = report.Asset.IpAddress,
                OsVersion = report.Asset.OsVersion,
                IsActive = report.Asset.IsActive,
            };

            List<VulnerabilityDetailDto> vulnerabilities = report
                .ReportVulnerabilities.Select(rv => new VulnerabilityDetailDto
                {
                    Id = rv.Vulnerability.Id,
                    CveId = rv.Vulnerability.CveId,
                    Description = rv.Vulnerability.Description,
                    CvsScore = rv.Vulnerability.CvsScore,
                    PublishedAt = rv.Vulnerability.PublishedAt,
                    SeverityLevel = rv.Vulnerability.SeverityLevel,
                })
                .ToList();

            ReportDetailsDto reportDetails = new ReportDetailsDto
            {
                Id = report.Id,
                CreatedAt = report.CreatedAt,
                IsCompleted = false, // For MVP, all reports are considered not completed
                Asset = assetDto,
                Vulnerabilities = vulnerabilities,
            };

            return Ok(reportDetails);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
