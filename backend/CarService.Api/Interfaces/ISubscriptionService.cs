using CarService.Api.DTOs;
using CarService.Api.Models;

namespace CarService.Api.Interfaces;

public interface ISubscriptionService
{
    Task<Subscription> CreateSubscriptionAsync(CreateSubscriptionRequest request);
    Task<IReadOnlyList<WorkOrder>> GenerateDailyWorkOrdersAsync(DateOnly serviceDate);
}
