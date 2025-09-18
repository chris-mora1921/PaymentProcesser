using Microsoft.AspNetCore.Mvc;
using Payment.Processor.Api.Models;
using Payment.Processor.Api.Services;

namespace Payment.Processor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IPaymentProcessorService paymentProcessorService) : ControllerBase
{
    [HttpGet("daily-totals/sample")]
    public IActionResult GetSampleDailyTotals()
    {
        IEnumerable<Transaction> sampleTransactions =
        [
            new(100.50m, "USD", new DateTime(2025, 9, 16, 10, 30, 0)),
            new(75.25m, "USD", new DateTime(2025, 9, 16, 14, 15, 0)),
            new(200.00m, "EUR", new DateTime(2025, 9, 16, 9, 45, 0)),
            new(50.75m, "USD", new DateTime(2025, 9, 15, 16, 20, 0)),
            new(125.00m, "EUR", new DateTime(2025, 9, 15, 11, 10, 0)),
            new(300.25m, "GBP", new DateTime(2025, 9, 16, 13, 30, 0))
        ];

        var result = paymentProcessorService.CalculateDailyTotals(sampleTransactions);

        return Ok(result);
    }
}
