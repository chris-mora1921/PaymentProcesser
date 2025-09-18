using Payment.Processor.Api.Models;

namespace Payment.Processor.Api.Services;

public interface IPaymentProcessorService
{
    Dictionary<string, Dictionary<DateTime, decimal>> CalculateDailyTotals(IEnumerable<Transaction> transactions);
}
