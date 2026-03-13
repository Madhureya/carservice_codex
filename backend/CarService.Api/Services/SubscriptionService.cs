using CarService.Api.Data;
using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using CarService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Services;

public class SubscriptionService(AppDbContext dbContext) : ISubscriptionService
{
    public async Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionRequest request)
    {
        var plan = await dbContext.ServicePlans.FirstOrDefaultAsync(x => x.Id == request.ServicePlanId && x.IsActive)
                   ?? throw new InvalidOperationException("Plan not found.");

        var subscription = new Subscription
        {
            UserId = request.UserId,
            VehicleId = request.VehicleId,
            ServicePlanId = request.ServicePlanId,
            StartDateUtc = request.StartDateUtc,
            EndDateUtc = plan.Frequency == PlanFrequency.OneTime ? request.StartDateUtc.AddMonths(1) : null,
            RemainingVisits = plan.VisitsPerCycle,
            Status = SubscriptionStatus.Active
        };

        dbContext.Subscriptions.Add(subscription);
        await dbContext.SaveChangesAsync();
        return subscription;
    }

    public async Task<IReadOnlyList<WorkOrder>> GenerateDailyWorkOrdersAsync(DateOnly serviceDate)
    {
        var dayOfWeek = (int)serviceDate.DayOfWeek;
        var activeSubscriptions = await dbContext.Subscriptions
            .Include(x => x.ServicePlan)
            .Where(x => x.Status == SubscriptionStatus.Active && x.RemainingVisits > 0)
            .ToListAsync();

        var toCreate = activeSubscriptions.Where(x =>
            x.ServicePlan.Frequency == PlanFrequency.Daily ||
            (x.ServicePlan.Frequency == PlanFrequency.Weekly && dayOfWeek is 1 or 4) ||
            (x.ServicePlan.Frequency == PlanFrequency.OneTime && DateOnly.FromDateTime(x.StartDateUtc) == serviceDate));

        var created = new List<WorkOrder>();

        foreach (var subscription in toCreate)
        {
            var exists = await dbContext.WorkOrders.AnyAsync(w => w.SubscriptionId == subscription.Id && w.ServiceDate == serviceDate);
            if (exists) continue;

            var order = new WorkOrder
            {
                SubscriptionId = subscription.Id,
                ServiceDate = serviceDate,
                Status = WorkOrderStatus.Pending
            };

            created.Add(order);
            dbContext.WorkOrders.Add(order);
        }

        await dbContext.SaveChangesAsync();
        return created;
    }
}
