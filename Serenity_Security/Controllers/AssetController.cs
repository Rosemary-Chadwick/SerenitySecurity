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
public class AssetController : ControllerBase
{
    private Serenity_SecurityDbContext _dbContext;

    public AssetController(Serenity_SecurityDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public IActionResult Get()
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

            List<AssetDto> assets = _dbContext
                .Assets.Include(a => a.SystemType)
                .Where(a => a.UserId == userProfile.Id)
                .Select(a => new AssetDto
                {
                    Id = a.Id,
                    SystemName = a.SystemName,
                    IpAddress = a.IpAddress,
                    OsVersion = a.OsVersion,
                    SystemTypeName = a.SystemType.Name,
                    IsActive = a.IsActive,
                })
                .ToList();
            return Ok(assets);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
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

            Asset asset = _dbContext
                .Assets.Include(a => a.SystemType)
                .Include(a => a.Reports)
                .FirstOrDefault(a => a.Id == id && a.UserId == userProfile.Id);

            if (asset == null)
            {
                return NotFound();
            }

            AssetDetailDto assetDetailDto = new AssetDetailDto
            {
                Id = asset.Id,
                SystemName = asset.SystemName,
                IpAddress = asset.IpAddress,
                OsVersion = asset.OsVersion,
                SystemTypeName = asset.SystemType.Name,
                IsActive = asset.IsActive,
                CreatedAt = asset.CreatedAt,
                Reports = asset
                    .Reports.Select(r => new ReportSummaryDto
                    {
                        Id = r.Id,
                        CreatedAt = r.CreatedAt,
                        IsCompleted = r.IsCompleted,
                    })
                    .ToList(),
            };
            return Ok(assetDetailDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
