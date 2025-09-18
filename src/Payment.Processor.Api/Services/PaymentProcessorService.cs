using Payment.Processor.Api.Models;

namespace Payment.Processor.Api.Services;

public class PaymentProcessorService : IPaymentProcessorService
{
    //probably more performant less readable 

    //public Dictionary<string, Dictionary<DateTime, decimal>> CalculateDailyTotals(IEnumerable<Transaction> transactions)
    //{
    //    var result = new Dictionary<string, Dictionary<DateTime, decimal>>(transactions.Count());

    //    foreach (var transaction in transactions)
    //    {
    //        var dateOnly = transaction.Timestamp.Date;

    //        if (!result.TryGetValue(transaction.Currency, out var currencyTotals))
    //        {
    //            currencyTotals = new Dictionary<DateTime, decimal>();
    //            result[transaction.Currency] = currencyTotals;
    //        }

    //        currencyTotals.TryGetValue(dateOnly, out var currentTotal);
    //        currencyTotals[dateOnly] = currentTotal + transaction.Amount;
    //    }

    //    return result;
    //}
    public Dictionary<string, Dictionary<DateTime, decimal>> CalculateDailyTotals(IEnumerable<Transaction> transactions)
    {
        return transactions
            .GroupBy(t => t.Currency)
            .ToDictionary(
                g => g.Key,
                g => g.GroupBy(t => t.Timestamp.Date)
                      .ToDictionary(dateGroup => dateGroup.Key, dateGroup => dateGroup.Sum(t => t.Amount))
            );
    }
}
