using CarService.Api.Data;
using CarService.Api.DTOs;
using CarService.Api.Interfaces;
using CarService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Services;

public class AnalyticsService(AppDbContext dbContext) : IAnalyticsService
{
    public async Task<DashboardMetrics> GetDashboardAsync()
    {
        var activeCustomers = await dbContext.Users.CountAsync(x => x.Role == UserRole.EndUser && x.IsActive);
        var activeSubscriptions = await dbContext.Subscriptions.CountAsync(x => x.Status == SubscriptionStatus.Active);
        var pendingOrders = await dbContext.WorkOrders.CountAsync(x => x.Status == WorkOrderStatus.Pending || x.Status == WorkOrderStatus.Assigned);

        var monthStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        var revenue = await dbContext.Payments.Where(x => x.PaidOnUtc >= monthStart).SumAsync(x => (decimal?)x.Amount) ?? 0m;

        return new DashboardMetrics(activeCustomers, activeSubscriptions, pendingOrders, revenue);
    }
}
