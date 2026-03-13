namespace CarService.Api.DTOs;

public record CreateSubscriptionRequest(Guid UserId, Guid VehicleId, int ServicePlanId, DateTime StartDateUtc);
public record AssignWorkOrderRequest(Guid WorkOrderId, Guid StaffId, Guid AssignedById);
public record CompleteWorkOrderRequest(Guid WorkOrderId, string Notes, string? BeforeImageUrl, string? AfterImageUrl);
