# Test Projects

This directory contains all test projects for the TintTrack application.

## Test Projects Structure

### 1. WTE.TintTrack.Business.Tests
**Purpose**: Unit tests for Business Application layer services

**Focus Areas**:
- Service layer unit tests
- Business logic validation
- DTO mapping tests
- Mock-based testing for isolated components

**Example**: `Services/CustomerServiceTests.cs`

### 2. WTE.TintTrack.Core.Tests
**Purpose**: Unit tests for Core Application layer services

**Focus Areas**:
- Core domain services (Tenant, User, Token, etc.)
- Authentication and authorization logic
- Core business rules
- Mock-based testing

**Example**: `Services/TenantServiceTests.cs`

### 3. WTE.TintTrack.Infrastructure.Tests
**Purpose**: Integration tests for Infrastructure layer (Repositories)

**Focus Areas**:
- Repository pattern implementation
- Database operations (using in-memory database)
- Entity Framework Core integration
- Data persistence and retrieval

**Example**: `Repositories/CustomerRepositoryTests.cs`

### 4. WTE.TintTrack.Api.Tests
**Purpose**: Integration tests for API controllers

**Focus Areas**:
- API endpoint testing
- HTTP request/response validation
- End-to-end workflow testing
- Authentication and authorization at API level

**Example**: `Controllers/CustomerControllerTests.cs`

## Running Tests

### Visual Studio
- Right-click solution → Run Tests
- Use Test Explorer window
- Code coverage analysis available

### Command Line

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test _Tests/WTE.TintTrack.Business.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test
dotnet test --filter "FullyQualifiedName~CustomerServiceTests"
```

## Test Naming Conventions

- **Test classes**: `[ClassUnderTest]Tests`
- **Test methods**: `[MethodUnderTest]_[Scenario]_[ExpectedResult]`

Example:
```csharp
public class CustomerServiceTests
{
    [Fact]
    public async Task GetByCodeAsync_ReturnsCustomer_WhenExists() { }
    
    [Fact]
    public async Task GetByCodeAsync_ReturnsNull_WhenNotFound() { }
}
```

## Testing Best Practices

1. **Arrange-Act-Assert** - Structure tests clearly
2. **One Assert Per Test** - When possible, test one thing
3. **Descriptive Names** - Test names should describe what they test
4. **Independent Tests** - Tests should not depend on each other
5. **Fast Tests** - Unit tests should run quickly
6. **Mock External Dependencies** - Don't mock what you own
7. **Test Behavior, Not Implementation** - Focus on what, not how

## Dependencies

- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library
- **Microsoft.EntityFrameworkCore.InMemory** - In-memory database for integration tests
- **Microsoft.AspNetCore.Mvc.Testing** - API testing support

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

For more detailed information, see `Documentations/TESTING_INFRASTRUCTURE.md`.

