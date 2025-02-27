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
}
