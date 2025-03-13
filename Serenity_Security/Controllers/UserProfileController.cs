using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Data;
using Serenity_Security.Models.DTOs;

namespace Serenity_Security.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private Serenity_SecurityDbContext _dbContext;

    public UserProfileController(Serenity_SecurityDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(
            _dbContext
                .UserProfiles.Include(up => up.IdentityUser)
                .Select(up => new UserProfileDto
                {
                    Id = up.Id,
                    Email = up.IdentityUser.Email,
                    Username = up.IdentityUser.UserName,
                })
                .ToList()
        );
    }

    [HttpGet("current")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userProfile = _dbContext
            .UserProfiles.Include(up => up.IdentityUser)
            .FirstOrDefault(up => up.IdentityUserId == userId);

        if (userProfile == null)
        {
            return NotFound();
        }

        return Ok(
            new UserProfileDto
            {
                Id = userProfile.Id,
                Email = userProfile.IdentityUser.Email,
                Username = userProfile.Username,
                IsAdmin = userProfile.IsAdmin,
            }
        );
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        var userProfile = _dbContext
            .UserProfiles.Include(up => up.IdentityUser)
            .FirstOrDefault(up => up.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }

        // Check if user is requesting their own profile or is admin
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userProfile.IdentityUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        return Ok(
            new UserProfileDto
            {
                Id = userProfile.Id,
                Email = userProfile.IdentityUser.Email,
                Username = userProfile.Username,
                IsAdmin = userProfile.IsAdmin,
            }
        );
    }

    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, UserUpdateDto userUpdate)
    {
        var userProfile = _dbContext
            .UserProfiles.Include(up => up.IdentityUser)
            .FirstOrDefault(up => up.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }

        // Check if user is updating their own profile or is admin
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userProfile.IdentityUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }
        // Update properties
        userProfile.Username = userUpdate.Username;

        // Update identity user email if changed
        if (userProfile.IdentityUser.Email != userUpdate.Email)
        {
            userProfile.IdentityUser.Email = userUpdate.Email;
            userProfile.IdentityUser.NormalizedEmail = userUpdate.Email.ToUpper();
        }

        _dbContext.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var userProfile = _dbContext
            .UserProfiles.Include(up => up.IdentityUser)
            .Include(up => up.Assets)
            .ThenInclude(a => a.Reports)
            .FirstOrDefault(up => up.Id == id);

        if (userProfile == null)
        {
            return NotFound();
        }

        // Check if user is deleting their own profile or is admin
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userProfile.IdentityUserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        // Delete associated assets and reports
        foreach (var asset in userProfile.Assets)
        {
            foreach (var report in asset.Reports)
            {
                _dbContext.Reports.Remove(report);
            }
            _dbContext.Assets.Remove(asset);
        }

        // Delete the user profile and identity user
        _dbContext.UserProfiles.Remove(userProfile);
        _dbContext.Users.Remove(userProfile.IdentityUser);

        _dbContext.SaveChanges();
        return NoContent();
    }
}
