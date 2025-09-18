# Payment Processor API

A .NET 8 Web API that calculates daily payment totals grouped by currency and date.

## Getting Started

1. Run the application:
   ```powershell
   cd src
   dotnet run --project Payment.Processor.Api
   ```

2. Open Swagger UI (launches automatically): `https://localhost:53514/swagger`

3. Run tests:
   ```powershell
   dotnet test
   ```

## API Endpoint

### GET `/api/payment/daily-totals/sample`
Returns sample daily totals using predefined transaction data.

**Response:**
```json
{
  "USD": {
    "2025-09-15T00:00:00": 50.75,
    "2025-09-16T00:00:00": 175.75
  },
  "EUR": {
    "2025-09-15T00:00:00": 125.00,
    "2025-09-16T00:00:00": 200.00
  },
  "GBP": {
    "2025-09-16T00:00:00": 300.25
  }
}
```

## Architecture

- **Transaction Model**: Amount, Currency, Timestamp
- **PaymentProcessorService**: Groups transactions by currency and date, sums daily totals
- **PaymentController**: REST API endpoint
- **GlobalExceptionMiddleware**: Centralized error handling
- **Swagger UI**: Interactive API documentation

## Testing

- **Unit Tests**: Core business logic (PaymentProcessorService)
- **Integration Tests**: End-to-end API functionality

## Tech Stack

- .NET 8 + ASP.NET Core
- Swashbuckle (Swagger)
- xUnit + Moq + FluentAssertions
