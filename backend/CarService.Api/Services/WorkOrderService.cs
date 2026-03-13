using CarService.Api.Data;
using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using CarService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Services;

public class WorkOrderService(AppDbContext dbContext) : IWorkOrderService
{
    public async Task<WorkOrder?> AssignAsync(AssignWorkOrderRequest request)
    {
        var order = await dbContext.WorkOrders.FirstOrDefaultAsync(x => x.Id == request.WorkOrderId);
        if (order is null) return null;

        order.AssignedStaffId = request.StaffId;
        order.AssignedById = request.AssignedById;
        order.Status = WorkOrderStatus.Assigned;
        await dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<WorkOrder?> CompleteAsync(CompleteWorkOrderRequest request)
    {
        var order = await dbContext.WorkOrders.Include(x => x.Subscription).FirstOrDefaultAsync(x => x.Id == request.WorkOrderId);
        if (order is null) return null;

        order.Status = WorkOrderStatus.Completed;
        order.Notes = request.Notes;
        order.BeforeImageUrl = request.BeforeImageUrl;
        order.AfterImageUrl = request.AfterImageUrl;
        order.CompletedAtUtc = DateTime.UtcNow;
        order.Subscription.RemainingVisits = Math.Max(0, order.Subscription.RemainingVisits - 1);

        await dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<IReadOnlyList<WorkOrder>> GetByDateAsync(DateOnly date)
    {
        return await dbContext.WorkOrders
            .Include(x => x.Subscription)
            .ThenInclude(x => x.Vehicle)
            .Include(x => x.AssignedStaff)
            .Where(x => x.ServiceDate == date)
            .OrderBy(x => x.Status)
            .ToListAsync();
    }
}
