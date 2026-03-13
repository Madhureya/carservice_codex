using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkOrdersController(IWorkOrderService workOrderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByDate([FromQuery] DateOnly? date)
    {
        var orders = await workOrderService.GetByDateAsync(date ?? DateOnly.FromDateTime(DateTime.UtcNow));
        return Ok(orders);
    }

    [HttpPost("assign")]
    [Authorize(Roles = "Supervisor,Manager,Admin")]
    public async Task<IActionResult> Assign(AssignWorkOrderRequest request)
    {
        var order = await workOrderService.AssignAsync(request);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost("complete")]
    [Authorize(Roles = "Staff,Supervisor,Manager,Admin")]
    public async Task<IActionResult> Complete(CompleteWorkOrderRequest request)
    {
        var order = await workOrderService.CompleteAsync(request);
        return order is null ? NotFound() : Ok(order);
    }
}
