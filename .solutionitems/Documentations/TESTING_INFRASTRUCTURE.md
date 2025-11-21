# Testing Infrastructure Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the testing infrastructure and best practices for the TintTrack application.

## Testing Strategy

### Test Pyramid

```
        /\
       /  \      E2E Tests (Few)
      /____\
     /      \    Integration Tests (Some)
    /________\
   /          \  Unit Tests (Many)
  /____________\
```

1. **Unit Tests** - Test individual components in isolation
2. **Integration Tests** - Test components working together
3. **E2E Tests** - Test complete user workflows

## Unit Testing

### Testing Services

```csharp
using Moq;
using Xunit;
using FluentAssertions;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CustomerService>> _loggerMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _repositoryMock = new Mock<ICustomerRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CustomerService>>();
        _service = new CustomerService(
            _repositoryMock.Object,
            _mapperMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCustomer_WhenExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer { Id = customerId, Code = "C001", Name = "Test" };
        var customerDto = new CustomerDto { Id = customerId, Code = "C001", Name = "Test" };

        _repositoryMock.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync(customer);
        _mapperMock.Setup(m => m.Map<CustomerDto>(customer))
            .Returns(customerDto);

        // Act
        var result = await _service.GetByIdAsync(customerId);

        // Assert
        result.Should().NotBeNull();
        result.Code.Should().Be("C001");
        _repositoryMock.Verify(r => r.GetByIdAsync(customerId), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(customerId))
            .ReturnsAsync((Customer?)null);

        // Act
        var result = await _service.GetByIdAsync(customerId);

        // Assert
        result.Should().BeNull();
    }
}
```

### Testing with Result Pattern

```csharp
[Fact]
public async Task CreateCustomerAsync_ReturnsSuccessResult_WhenValid()
{
    // Arrange
    var customerDto = new CustomerDto { Code = "C001", Name = "Test" };
    var customer = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test" };

    _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Customer>()));
    _mapperMock.Setup(m => m.Map<Customer>(customerDto)).Returns(customer);

    // Act
    var result = await _service.CreateCustomerAsync(customerDto);

    // Assert
    result.IsSuccess.Should().BeTrue();
    result.Value.Should().NotBeNull();
}

[Fact]
public async Task CreateCustomerAsync_ReturnsFailureResult_WhenDuplicateCode()
{
    // Arrange
    var customerDto = new CustomerDto { Code = "C001", Name = "Test" };
    _repositoryMock.Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
        .ReturnsAsync(true);

    // Act
    var result = await _service.CreateCustomerAsync(customerDto);

    // Assert
    result.IsFailure.Should().BeTrue();
    result.ErrorCode.Should().Be("CONFLICT");
}
```

## Integration Testing

### Testing Repositories with In-Memory Database

```csharp
using Microsoft.EntityFrameworkCore;

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
        var customer = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test" };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(customer.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Code.Should().Be("C001");
    }

    [Fact]
    public async Task AddAsync_AddsCustomer_ToDatabase()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test" };

        // Act
        await _repository.AddAsync(customer);
        await _repository.CommitAsync();

        // Assert
        var result = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
        result.Should().NotBeNull();
        result!.Code.Should().Be("C001");
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
```

### Testing Unit of Work with Transactions

```csharp
[Fact]
public async Task ExecuteInTransactionAsync_RollsBack_OnError()
{
    // Arrange
    var customer1 = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test1" };
    var customer2 = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test2" }; // Duplicate code

    // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _customerRepository.AddAsync(customer1);
                await _customerRepository.AddAsync(customer2); // This will fail due to unique constraint
            });
        });

    // Verify rollback - no customers should be saved
    var count = await _context.Customers.CountAsync();
    count.Should().Be(0);
}
```

## API Integration Testing

### Testing Controllers

```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

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
                // Replace real database with in-memory
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TenantDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<TenantDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
        });
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomer_ReturnsOk_WhenExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        // Setup test data...

        // Act
        var response = await _client.GetAsync($"/api/customer/{customerId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("C001");
    }
}
```

## Testing Domain Events

```csharp
[Fact]
public void CreateCustomer_RaisesCustomerCreatedEvent()
{
    // Arrange & Act
    var customer = Customer.Create("C001", "Test Customer");

    // Assert
    customer.DomainEvents.Should().HaveCount(1);
    customer.DomainEvents.First().Should().BeOfType<CustomerCreatedEvent>();
}

[Fact]
public async Task HandleAsync_LogsAuditEntry_WhenCustomerCreated()
{
    // Arrange
    var auditServiceMock = new Mock<IAuditLogService>();
    var handler = new CustomerCreatedEventHandler(auditServiceMock.Object, _logger);
    var @event = new CustomerCreatedEvent(Guid.NewGuid(), "C001", "Test", Guid.NewGuid());

    // Act
    await handler.HandleAsync(@event);

    // Assert
        auditServiceMock.Verify(s => s.LogAsync(It.IsAny<AuditLogDto>()), Times.Once);
}
```

## Test Data Builders

### Using Builder Pattern for Test Data

```csharp
public class CustomerBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _code = "C001";
    private string _name = "Test Customer";
    private bool _isActive = true;

    public CustomerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CustomerBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CustomerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CustomerBuilder Inactive()
    {
        _isActive = false;
        return this;
    }

    public Customer Build()
    {
        return new Customer
        {
            Id = _id,
            Code = _code,
            Name = _name,
            IsActive = _isActive
        };
    }
}

// Usage
var customer = new CustomerBuilder()
    .WithCode("C002")
    .WithName("Another Customer")
    .Build();
```

## Mocking Best Practices

### 1. Use Mock Only When Necessary

```csharp
// Good: Mock external dependencies
var emailServiceMock = new Mock<IEmailService>();

// Avoid: Mock simple value objects or DTOs
var customerDtoMock = new Mock<CustomerDto>(); // Don't mock DTOs
```

### 2. Verify Interactions

```csharp
[Fact]
public async Task CreateCustomer_CallsRepositoryAdd()
{
    // Arrange
    var customerDto = new CustomerDto { Code = "C001", Name = "Test" };

    // Act
    await _service.CreateCustomerAsync(customerDto);

    // Assert
    _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
    _repositoryMock.Verify(r => r.CommitAsync(), Times.Once);
}
```

### 3. Setup Return Values Properly

```csharp
// Good: Setup with specific conditions
_repositoryMock.Setup(r => r.GetByIdAsync(It.Is<Guid>(id => id == customerId)))
    .ReturnsAsync(customer);

// Avoid: Too generic
_repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
    .ReturnsAsync(customer); // May match unexpected calls
```

## Test Organization

### Project Structure

```
_Tests/
  ├── WTE.TintTrack.Core.Tests/
  │   ├── Services/
  │   ├── Repositories/
  │   └── Handlers/
  ├── WTE.TintTrack.Business.Tests/
  │   ├── Services/
  │   ├── Repositories/
  │   └── Handlers/
  └── WTE.TintTrack.Api.Tests/
      ├── Controllers/
      ├── Integration/
      └── E2E/
```

### Naming Conventions

- Test classes: `[ClassUnderTest]Tests`
- Test methods: `[MethodUnderTest]_[Scenario]_[ExpectedResult]`

```csharp
public class CustomerServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsCustomer_WhenExists() { }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound() { }
}
```

## Test Categories

### Unit Tests

- Fast execution (< 100ms per test)
- No external dependencies
- Test single component
- Use mocks for dependencies

### Integration Tests

- Slower execution (100ms - 1s per test)
- Use real database (in-memory or test database)
- Test component interactions
- May use real dependencies

### E2E Tests

- Slowest execution (> 1s per test)
- Use real infrastructure
- Test complete workflows
- Minimal mocking

## Running Tests

### Visual Studio

- Right-click solution → Run Tests
- Test Explorer window
- Code coverage analysis

### Command Line

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test _Tests/WTE.TintTrack.Core.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test
dotnet test --filter "FullyQualifiedName~CustomerServiceTests"
```

## Continuous Integration

### GitHub Actions Example

```yaml
name: Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '9.0.x'
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Test
        run: dotnet test --no-build --verbosity normal
```

## Best Practices Summary

1. **Arrange-Act-Assert** - Structure tests clearly
2. **One Assert Per Test** - When possible, test one thing
3. **Descriptive Names** - Test names should describe what they test
4. **Independent Tests** - Tests should not depend on each other
5. **Fast Tests** - Unit tests should run quickly
6. **Use Test Data Builders** - Reduce test setup code
7. **Mock External Dependencies** - Don't mock what you own
8. **Test Behavior, Not Implementation** - Focus on what, not how
9. **Maintain Test Code** - Keep tests clean and maintainable
10. **Aim for High Coverage** - But focus on meaningful tests

