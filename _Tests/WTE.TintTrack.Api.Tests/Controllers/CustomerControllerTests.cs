using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using WTE.TintTrack.Business.Infrastructure;
using Xunit;

namespace WTE.TintTrack.Api.Tests.Controllers;

public class CustomerControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CustomerControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace real database with in-memory for testing
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TenantDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TenantDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid().ToString());
                });
            });
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomer_ReturnsUnauthorized_WhenNotAuthenticated()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/v1/Customer/{customerId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    // Note: Additional tests would require authentication setup
    // This is a basic structure showing how to set up API integration tests
}

