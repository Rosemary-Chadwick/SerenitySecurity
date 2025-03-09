using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serenity_Security.Data;
using Serenity_Security.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class SystemTypesController : ControllerBase
{
    private readonly Serenity_SecurityDbContext _dbContext;

    public SystemTypesController(Serenity_SecurityDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SystemTypeDto>>> GetSystemType()
    {
        return await _dbContext
            .SystemTypes.Select(st => new SystemTypeDto { Id = st.Id, Name = st.Name })
            .ToListAsync();
    }
}
