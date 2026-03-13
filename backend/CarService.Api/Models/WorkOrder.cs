namespace CarService.Api.Models;

public class WorkOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubscriptionId { get; set; }
    public DateOnly ServiceDate { get; set; }
    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;
    public Guid? AssignedStaffId { get; set; }
    public Guid? AssignedById { get; set; }
    public string Notes { get; set; } = string.Empty;
    public string? BeforeImageUrl { get; set; }
    public string? AfterImageUrl { get; set; }
    public DateTime? CompletedAtUtc { get; set; }

    public Subscription Subscription { get; set; } = null!;
    public User? AssignedStaff { get; set; }
}
