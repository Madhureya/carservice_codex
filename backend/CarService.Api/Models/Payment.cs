namespace CarService.Api.Models;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidOnUtc { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public string PaymentMode { get; set; } = string.Empty;

    public Subscription Subscription { get; set; } = null!;
}
