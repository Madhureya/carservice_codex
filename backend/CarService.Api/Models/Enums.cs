namespace CarService.Api.Models;

public enum UserRole
{
    EndUser = 1,
    Staff = 2,
    Supervisor = 3,
    Manager = 4,
    Admin = 5
}

public enum VehicleType
{
    Car = 1,
    Bike = 2
}

public enum PlanFrequency
{
    Daily = 1,
    Weekly = 2,
    OneTime = 3
}

public enum SubscriptionStatus
{
    Active = 1,
    Paused = 2,
    Cancelled = 3
}

public enum WorkOrderStatus
{
    Pending = 1,
    Assigned = 2,
    InProgress = 3,
    Completed = 4,
    Skipped = 5
}
