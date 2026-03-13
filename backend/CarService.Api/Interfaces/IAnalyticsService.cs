using CarService.Api.DTOs;

namespace CarService.Api.Interfaces;

public interface IAnalyticsService
{
    Task<DashboardMetrics> GetDashboardAsync();
}
