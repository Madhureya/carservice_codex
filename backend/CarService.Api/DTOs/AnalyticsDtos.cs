namespace CarService.Api.DTOs;

public record DashboardMetrics(int ActiveCustomers, int ActiveSubscriptions, int PendingWorkOrders, decimal MonthlyRevenue);
