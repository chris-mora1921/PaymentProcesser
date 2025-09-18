namespace Payment.Processor.Api.Models;

public class Transaction(decimal amount, string currency, DateTime timestamp)
{
    public decimal Amount { get; set; } = amount;
    public string Currency { get; set; } = currency;
    public DateTime Timestamp { get; set; } = timestamp;
}
