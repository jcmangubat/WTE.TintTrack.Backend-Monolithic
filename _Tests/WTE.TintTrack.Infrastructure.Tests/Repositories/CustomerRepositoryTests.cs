using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Infrastructure;
using WTE.TintTrack.Business.Infrastructure.Repositories;
using Xunit;

namespace WTE.TintTrack.Infrastructure.Tests.Repositories;

public class CustomerRepositoryTests : IDisposable
{
    private readonly TenantDbContext _context;
    private readonly CustomerRepository _repository;

    public CustomerRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new TenantDbContext(options);
        _repository = new CustomerRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCustomer_WhenExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer 
        { 
            Id = customerId, 
            Code = "C001", 
            Name = "Test Customer",
            MainPhone = "123-456-7890"
        };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetSingleAsync(c => c.Id == customerId);

        // Assert
        result.Should().NotBeNull();
        result!.Code.Should().Be("C001");
        result.Name.Should().Be("Test Customer");
    }

    [Fact]
    public async Task GetSingleAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var customerId = Guid.NewGuid();

        // Act
        var result = await _repository.GetSingleAsync(c => c.Id == customerId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_AddsCustomer_ToDatabase()
    {
        // Arrange
        var customer = new Customer 
        { 
            Id = Guid.NewGuid(), 
            Code = "C002", 
            Name = "New Customer",
            MainPhone = "123-456-7890"
        };

        // Act
        await _repository.AddAsync(customer);
        await _repository.CommitAsync();

        // Assert
        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
        result.Should().NotBeNull();
        result!.Code.Should().Be("C002");
        result.Name.Should().Be("New Customer");
    }

    [Fact]
    public async Task GetSingleAsync_ReturnsCustomer_WhenCodeMatches()
    {
        // Arrange
        var customer = new Customer 
        { 
            Id = Guid.NewGuid(), 
            Code = "C003", 
            Name = "Code Test Customer",
            MainPhone = "123-456-7890"
        };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetSingleAsync(c => c.Code == "C003");

        // Assert
        result.Should().NotBeNull();
        result!.Code.Should().Be("C003");
        result.Name.Should().Be("Code Test Customer");
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

