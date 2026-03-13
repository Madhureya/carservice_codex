namespace CarService.Api.Models;

public class ServicePlan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }
    public PlanFrequency Frequency { get; set; }
    public decimal Price { get; set; }
    public int VisitsPerCycle { get; set; }
    public bool IncludesInteriorCleaning { get; set; }
    public bool IncludesPolishing { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
