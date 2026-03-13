using CarService.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlansController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var plans = await dbContext.ServicePlans.Where(x => x.IsActive).ToListAsync();
        return Ok(plans);
    }
}
