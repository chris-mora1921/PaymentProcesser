using FluentAssertions;
using Payment.Processor.Api.Models;
using Payment.Processor.Api.Services;
using Xunit;

namespace Payment.Processor.Api.Unit.Tests.Services;

public class PaymentProcessorServiceTests
{
    private readonly PaymentProcessorService _service;

    public PaymentProcessorServiceTests()
    {
        _service = new PaymentProcessorService();
    }

    [Fact]
    public void CalculateDailyTotals_WithEmptyTransactions_ReturnsEmpty()
    {
        // Arrange
        var transactions = new List<Transaction>();

        // Act
        var result = _service.CalculateDailyTotals(transactions);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void CalculateDailyTotals_WithSingleTransaction_ReturnsCorrectTotal()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new(100.50m, "USD", new DateTime(2025, 9, 16, 10, 30, 0))
        };

        // Act
        var result = _service.CalculateDailyTotals(transactions);

        // Assert
        result.Should().ContainKey("USD");
        result["USD"].Should().ContainKey(new DateTime(2025, 9, 16));
        result["USD"][new DateTime(2025, 9, 16)].Should().Be(100.50m);
    }

    [Fact]
    public void CalculateDailyTotals_SameCurrencySameDay_SumsAmounts()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new(100.50m, "USD", new DateTime(2025, 9, 16, 10, 30, 0)),
            new(75.25m, "USD", new DateTime(2025, 9, 16, 14, 15, 0)),
            new(50.00m, "USD", new DateTime(2025, 9, 16, 18, 45, 0))
        };

        // Act
        var result = _service.CalculateDailyTotals(transactions);

        // Assert
        result.Should().ContainKey("USD");
        result["USD"].Should().ContainKey(new DateTime(2025, 9, 16));
        result["USD"][new DateTime(2025, 9, 16)].Should().Be(225.75m);
    }

    [Fact]
    public void CalculateDailyTotals_SameCurrencyDifferentDays_GroupsByDay()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new(100.50m, "USD", new DateTime(2025, 9, 16, 10, 30, 0)),
            new(75.25m, "USD", new DateTime(2025, 9, 15, 14, 15, 0)),
            new(50.00m, "USD", new DateTime(2025, 9, 17, 18, 45, 0))
        };

        // Act
        var result = _service.CalculateDailyTotals(transactions);

        // Assert
        result.Should().ContainKey("USD");
        result["USD"].Should().HaveCount(3);
        result["USD"][new DateTime(2025, 9, 15)].Should().Be(75.25m);
        result["USD"][new DateTime(2025, 9, 16)].Should().Be(100.50m);
        result["USD"][new DateTime(2025, 9, 17)].Should().Be(50.00m);
    }

    [Fact]
    public void CalculateDailyTotals_MultipleCurrencies_GroupsByCurrency()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new(100.50m, "USD", new DateTime(2025, 9, 16, 10, 30, 0)),
            new(200.00m, "EUR", new DateTime(2025, 9, 16, 14, 15, 0)),
            new(300.25m, "GBP", new DateTime(2025, 9, 16, 18, 45, 0))
        };

        // Act
        var result = _service.CalculateDailyTotals(transactions);

        // Assert
        result.Should().HaveCount(3);
        result.Should().ContainKeys("USD", "EUR", "GBP");
        result["USD"][new DateTime(2025, 9, 16)].Should().Be(100.50m);
        result["EUR"][new DateTime(2025, 9, 16)].Should().Be(200.00m);
        result["GBP"][new DateTime(2025, 9, 16)].Should().Be(300.25m);
    }
}
