using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Payment.Processor.Api.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace Payment.Processor.Api.Integration.Tests.Controllers;

public class PaymentControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public PaymentControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetSampleDailyTotals_ShouldReturnSampleData()
    {
        // Act
        var response = await _client.GetAsync("/api/payment/daily-totals/sample");

        // Assert
        response.Should().BeSuccessful();
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().NotBeNullOrEmpty();
        
        var result = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, decimal>>>(content, _jsonOptions);
        result.Should().NotBeNull();
        result.Should().ContainKeys("USD", "EUR", "GBP");
    }
}
