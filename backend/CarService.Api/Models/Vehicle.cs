namespace CarService.Api.Models;

public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }
    public string ParkingSpot { get; set; } = string.Empty;

    public User User { get; set; } = null!;
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
