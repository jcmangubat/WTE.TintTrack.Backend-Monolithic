# Database Context Lifecycle Management

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes how DbContext instances are managed throughout the application lifecycle to ensure proper resource management and tenant isolation.

## Context Types

### ApplicationDbContext (Core/Platform Database)
- **Lifetime:** Scoped (per HTTP request)
- **Purpose:** Platform-level data (users, tenants, subscriptions)
- **Connection:** Single shared database
- **Registration:** `services.AddDbContext<ApplicationDbContext>()`

### TenantDbContext (Business/Tenant Database)
- **Lifetime:** Scoped (per HTTP request)
- **Purpose:** Tenant-specific business data
- **Connection:** Dynamic per tenant (resolved via TenantContext)
- **Registration:** `services.AddDbContext<TenantDbContext>()`

## Lifecycle Management

### Scoped Lifetime
Both contexts are registered with scoped lifetime, meaning:
- One instance per HTTP request
- Automatically disposed at end of request
- Shared across all services in the same request scope

### Unit of Work Pattern
The Unit of Work pattern manages transactions and ensures:
- Single `SaveChangesAsync()` call per request (when using UoW)
- Proper transaction management
- Automatic rollback on errors

## Tenant Context Resolution

TenantDbContext connection string is resolved dynamically:

1. **TenantContextMiddleware** resolves tenant early in pipeline
2. **TenantContext** service provides tenant connection string
3. **TenantDbContextFactory** creates context with tenant-specific connection

## Best Practices

### 1. Use Unit of Work for Transactions
```csharp
// Good: Using Unit of Work
public async Task CreateCustomerAsync(CustomerDto dto)
{
    await _unitOfWork.ExecuteInTransactionAsync(async () =>
    {
        await _customerRepository.AddAsync(customer);
        await _addressRepository.AddAsync(address);
        // Single SaveChanges call
    });
}

// Avoid: Direct repository commits
public async Task CreateCustomerAsync(CustomerDto dto)
{
    await _customerRepository.AddAsync(customer);
    await _customerRepository.CommitAsync(); // Multiple commits
    await _addressRepository.AddAsync(address);
    await _addressRepository.CommitAsync();
}
```

### 2. Don't Create Contexts Manually
```csharp
// Bad: Manual context creation
using var context = new ApplicationDbContext(options);
// Context not managed by DI, may cause issues

// Good: Inject via constructor
public class MyService
{
    private readonly ApplicationDbContext _context;
    
    public MyService(ApplicationDbContext context)
    {
        _context = context; // Managed by DI
    }
}
```

### 3. Avoid Long-Running Contexts
```csharp
// Bad: Context held for long time
public class BackgroundService
{
    private readonly ApplicationDbContext _context; // Singleton service
    
    // Context will be held for entire service lifetime
}

// Good: Create context per operation
public class BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    
    public async Task ProcessAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Context disposed after operation
    }
}
```

### 4. Use Factory Pattern for Dynamic Contexts
```csharp
// For tenant-specific contexts created dynamically
public class TenantDbContextFactory
{
    public TenantDbContext Create(string connectionString)
    {
        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseSqlServer(connectionString)
            .Options;
        return new TenantDbContext(options);
    }
}
```

## Context Disposal

DbContexts are automatically disposed:
- At end of HTTP request (scoped lifetime)
- When Unit of Work is disposed
- When using statement completes (if manually created)

## Connection Pooling

Entity Framework Core manages connection pooling automatically:
- Connections are pooled and reused
- Pool size managed by database provider
- No manual connection management needed

## Performance Considerations

1. **Query Splitting:** Configured to split queries for multiple collections
2. **Retry on Failure:** Enabled for transient database errors
3. **Change Tracking:** Disabled for read-only queries when appropriate
4. **Async Operations:** All database operations use async/await

## Testing

For testing, use in-memory databases:

```csharp
services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("TestDatabase");
});
```

## Troubleshooting

### Context Disposed Errors
- Ensure context is scoped, not singleton
- Don't access context after request completes
- Use async/await properly

### Connection String Issues
- Verify tenant context is resolved before accessing TenantDbContext
- Check connection string template configuration
- Ensure tenant exists in database

### Transaction Issues
- Use Unit of Work for multi-step operations
- Don't mix direct repository commits with Unit of Work
- Ensure proper error handling and rollback

