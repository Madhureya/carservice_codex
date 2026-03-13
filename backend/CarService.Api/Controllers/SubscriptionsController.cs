using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager,Admin,Supervisor")]
public class SubscriptionsController(ISubscriptionService subscriptionService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateSubscriptionRequest request)
    {
        var response = await subscriptionService.CreateSubscriptionAsync(request);
        return Ok(response);
    }

    [HttpPost("generate-workorders")]
    public async Task<IActionResult> Generate([FromQuery] DateOnly? date)
    {
        var workOrders = await subscriptionService.GenerateDailyWorkOrdersAsync(date ?? DateOnly.FromDateTime(DateTime.UtcNow));
        return Ok(workOrders);
    }
}
