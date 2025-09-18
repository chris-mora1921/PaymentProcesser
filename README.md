# Payment Processor API

A .NET 8 Web API for processing payment transactions and calculating daily totals grouped by currency and date.

## Project Structure

```
├── README.md
└── src/
    ├── Payment.Processor.sln
    ├── Payment.Processor.Api/
    │   ├── Controllers/
    │   │   └── PaymentController.cs
    │   ├── Models/
    │   │   └── Transaction.cs
    │   ├── Services/
    │   │   ├── IPaymentProcessorService.cs
    │   │   └── PaymentProcessorService.cs
    │   ├── Payment.Processor.Api.csproj
    │   ├── Program.cs
    │   ├── appsettings.json
    │   └── appsettings.Development.json
    └── tests/
        ├── Payment.Processor.Api.Unit.Tests/
        │   ├── Controllers/
        │   │   └── PaymentControllerTests.cs
        │   ├── Models/
        │   │   └── TransactionTests.cs
        │   ├── Services/
        │   │   └── PaymentProcessorServiceTests.cs
        │   └── Payment.Processor.Api.Unit.Tests.csproj
        └── Payment.Processor.Api.Integration.Tests/
            ├── Controllers/
            │   └── PaymentControllerIntegrationTests.cs
            └── Payment.Processor.Api.Integration.Tests.csproj
```

## Features

- **Transaction Model**: Represents payment transactions with amount, currency, and timestamp
- **Daily Totals Calculation**: Groups transactions by currency and date, calculating daily totals
- **REST API**: Provides endpoints for processing transactions and retrieving sample data
- **Comprehensive Testing**: Includes both unit tests and integration tests
- **Dependency Injection**: Uses built-in DI container for service registration

## API Endpoints

### POST `/api/payment/daily-totals`
Calculates daily totals for the provided transactions.

**Request Body:**
```json
[
  {
    "amount": 100.50,
    "currency": "USD",
    "timestamp": "2025-09-16T10:30:00"
  },
  {
    "amount": 75.25,
    "currency": "USD",
    "timestamp": "2025-09-16T14:15:00"
  }
]
```

**Response:**
```json
{
  "USD": {
    "2025-09-16T00:00:00": 175.75
  }
}
```

### GET `/api/payment/daily-totals/sample`
Returns sample daily totals for demonstration purposes.

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

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. Navigate to the src directory:
   ```powershell
   cd src
   ```

2. Restore dependencies:
   ```powershell
   dotnet restore
   ```

3. Run the application:
   ```powershell
   dotnet run --project Payment.Processor.Api
   ```

4. Open your browser and navigate to:
   - API: `https://localhost:7000` (or the URL shown in console)
   - Swagger UI: `https://localhost:7000/swagger`

### Running Tests

#### Unit Tests
```powershell
dotnet test tests/Payment.Processor.Api.Unit.Tests
```

#### Integration Tests
```powershell
dotnet test tests/Payment.Processor.Api.Integration.Tests
```

#### All Tests
```powershell
dotnet test
```

### Build and Publish

#### Debug Build
```powershell
dotnet build
```

#### Release Build
```powershell
dotnet build --configuration Release
```

#### Publish
```powershell
dotnet publish --configuration Release --output ./publish
```

## Architecture

### Models
- **Transaction**: Core entity representing a payment transaction with amount, currency, and timestamp

### Services
- **IPaymentProcessorService**: Interface defining payment processing operations
- **PaymentProcessorService**: Implementation of payment processing logic, including the `CalculateDailyTotals` method

### Controllers
- **PaymentController**: REST API controller providing endpoints for transaction processing

### Key Algorithm
The `CalculateDailyTotals` method:
1. Groups transactions by currency
2. Within each currency, groups by date (ignoring time component)
3. Sums transaction amounts for each currency-date combination
4. Returns a nested dictionary structure: `Dictionary<string, Dictionary<DateTime, decimal>>`

## Testing Strategy

### Unit Tests
- **TransactionTests**: Tests for the Transaction model properties and constructors
- **PaymentProcessorServiceTests**: Tests for the core business logic, including edge cases and various scenarios
- **PaymentControllerTests**: Tests for API controller behavior, including error handling and service interaction

### Integration Tests
- **PaymentControllerIntegrationTests**: End-to-end tests for the complete API functionality, including HTTP request/response handling

### Test Coverage
- Model validation and property setting
- Service logic with various transaction scenarios
- API endpoint behavior and error handling
- HTTP request/response serialization
- Edge cases (null inputs, empty collections, etc.)

## Technologies Used

- **.NET 8**: Target framework
- **ASP.NET Core**: Web API framework
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **Moq**: Mocking framework for unit tests
- **FluentAssertions**: Assertion library for readable tests
- **Microsoft.AspNetCore.Mvc.Testing**: Integration testing framework

## Development Notes

- The API uses dependency injection for service registration
- All endpoints return JSON responses
- The time component is ignored when grouping by date
- Negative amounts are supported (for refunds/adjustments)
- Currency validation is not enforced (flexible for different currency formats)
- DateTime handling preserves the date part only for grouping purposes

## Future Enhancements

- Add currency validation
- Implement pagination for large transaction sets
- Add authentication and authorization
- Include transaction filtering capabilities
- Add logging and monitoring
- Implement caching for performance optimization
