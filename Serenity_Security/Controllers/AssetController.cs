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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AssetDto>> CreateAsset(AssetCreateUpdateDto assetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string identityUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(identityUserId))
        {
            return Unauthorized();
        }

        var userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(up =>
            up.IdentityUserId == identityUserId
        );

        if (userProfile == null)
        {
            return NotFound("User profile not found");
        }

        var asset = new Asset
        {
            UserId = userProfile.Id,
            SystemName = assetDto.SystemName,
            IpAddress = assetDto.IpAddress,
            OsVersion = assetDto.OsVersion,
            SystemTypeId = assetDto.SystemTypeId,
            IsActive = assetDto.IsActive,
            CreatedAt = DateTime.Now,
        };

        _dbContext.Assets.Add(asset);
        await _dbContext.SaveChangesAsync();

        var systemTypeName = await _dbContext
            .SystemTypes.Where(st => st.Id == asset.SystemTypeId)
            .Select(st => st.Name)
            .FirstOrDefaultAsync();

        var responseDto = new AssetDto
        {
            Id = asset.Id,
            SystemName = asset.SystemName,
            IpAddress = asset.IpAddress,
            OsVersion = asset.OsVersion,
            SystemTypeName = systemTypeName,
            IsActive = asset.IsActive,
        };

        return CreatedAtAction(nameof(GetById), new { id = asset.Id }, responseDto);
    } // CreatedAtAction - 201 with a Location header where the new asset can be found

    // nameof(GetById) - References another controller method by name (avoids hardcoding strings)
    // responseDto - The created asset object will be serialized in the response body


    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult> UpdateAsset(int id, AssetCreateUpdateDto assetDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var asset = await _dbContext.Assets.FindAsync(id); // optimized to first check the EF Core change tracker (in-memory cache) before hitting the database

        if (asset == null)
        {
            return NotFound("Asset not found");
        }

        string identityUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(identityUserId))
        {
            return Unauthorized();
        }

        var userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(up =>
            up.IdentityUserId == identityUserId
        );

        if (userProfile == null || asset.UserId != userProfile.Id)
        {
            return Forbid();
        }

        asset.SystemName = assetDto.SystemName;
        asset.IpAddress = assetDto.IpAddress;
        asset.OsVersion = assetDto.OsVersion;
        asset.SystemTypeId = assetDto.SystemTypeId;
        asset.IsActive = assetDto.IsActive;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) // The exception is thrown when Entity Framework detects that the data you're trying to update has been modified by another process since you retrieved it.
        {
            if (!await _dbContext.Assets.AnyAsync(a => a.Id == id)) // checks to see if data still exists... if it does then it throws another exception
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent(); // maybe do a return CreatedAtAction(nameof(GetById), new { id = asset.Id }, responseDto); here too... have to modify this method
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAsset(int id)
    {
        Asset assetToDelete = _dbContext.Assets.SingleOrDefault((a) => a.Id == id);
        if (assetToDelete == null)
        {
            return NotFound("Asset not found");
        }
        _dbContext.Assets.Remove(assetToDelete);
        _dbContext.SaveChanges();
        return NoContent();
    }
}
