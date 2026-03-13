using CarService.Api.DTOs;
using CarService.Api.Models;

namespace CarService.Api.Interfaces;

public interface IWorkOrderService
{
    Task<WorkOrder?> AssignAsync(AssignWorkOrderRequest request);
    Task<WorkOrder?> CompleteAsync(CompleteWorkOrderRequest request);
    Task<IReadOnlyList<WorkOrder>> GetByDateAsync(DateOnly date);
}
