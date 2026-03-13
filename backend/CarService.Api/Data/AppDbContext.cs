using CarService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ServicePlan> ServicePlans => Set<ServicePlan>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Vehicle>().HasIndex(x => x.RegistrationNumber).IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(x => x.Vehicles)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId);

        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Vehicle)
            .WithMany(v => v.Subscriptions)
            .HasForeignKey(x => x.VehicleId);

        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.ServicePlan)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(x => x.ServicePlanId);

        modelBuilder.Entity<WorkOrder>()
            .HasOne(x => x.AssignedStaff)
            .WithMany(u => u.AssignedWorkOrders)
            .HasForeignKey(x => x.AssignedStaffId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ServicePlan>().HasData(
            new ServicePlan { Id = 1, Name = "Daily Exterior Wash", Description = "Daily exterior cleaning", VehicleType = VehicleType.Car, Frequency = PlanFrequency.Daily, Price = 1499, VisitsPerCycle = 30, IncludesInteriorCleaning = false, IncludesPolishing = false },
            new ServicePlan { Id = 2, Name = "Weekly Full Wash", Description = "Exterior + interior weekly", VehicleType = VehicleType.Car, Frequency = PlanFrequency.Weekly, Price = 1199, VisitsPerCycle = 4, IncludesInteriorCleaning = true, IncludesPolishing = false },
            new ServicePlan { Id = 3, Name = "Monthly Polish", Description = "Premium wash + polish", VehicleType = VehicleType.Car, Frequency = PlanFrequency.OneTime, Price = 999, VisitsPerCycle = 1, IncludesInteriorCleaning = true, IncludesPolishing = true },
            new ServicePlan { Id = 4, Name = "Bike Daily Wash", Description = "Daily bike wash", VehicleType = VehicleType.Bike, Frequency = PlanFrequency.Daily, Price = 699, VisitsPerCycle = 30, IncludesInteriorCleaning = false, IncludesPolishing = false }
        );
    }
}
