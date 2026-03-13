namespace CarService.Api.Models;

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid VehicleId { get; set; }
    public int ServicePlanId { get; set; }
    public DateTime StartDateUtc { get; set; }
    public DateTime? EndDateUtc { get; set; }
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;
    public int RemainingVisits { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
    public ServicePlan ServicePlan { get; set; } = null!;
    public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
