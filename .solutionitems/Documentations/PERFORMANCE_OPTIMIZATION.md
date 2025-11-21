# Performance Optimization Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes performance optimization strategies and best practices for the TintTrack application.

## Caching Strategy

### In-Memory Caching

The application uses `IMemoryCache` for frequently accessed data:

```csharp
public class SubscriptionPlanService
{
    private readonly ICacheService _cacheService;
    
    public async Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync(bool excludeInActives = true)
    {
        var cacheKey = $"subscription_plans_{excludeInActives}";
        
        return await _cacheService.GetOrSetAsync(
            cacheKey,
            async () => await _subscriptionPlanRepository.GetAllAsync(excludeInActives),
            TimeSpan.FromHours(1)
        );
    }
}
```

### Cache Invalidation

When data changes, invalidate related cache entries:

```csharp
public async Task<SubscriptionPlanDto> UpdateAsync(Guid id, SubscriptionPlanDto dto)
{
    var result = await _subscriptionPlanRepository.UpdateAsync(id, dto);
    
    // Invalidate cache
    await _cacheService.RemoveAsync("subscription_plans_true");
    await _cacheService.RemoveAsync("subscription_plans_false");
    
    return result;
}
```

### Best Practices

1. **Cache Frequently Accessed, Rarely Changed Data**
   - Subscription plans
   - Configuration settings
   - Reference data (lookups, enums)

2. **Set Appropriate Expiration Times**
   - Static data: Hours or days
   - Frequently changing data: Minutes
   - User-specific data: Per-request or short-lived

3. **Use Cache Keys Consistently**
   - Include relevant parameters in cache key
   - Use consistent naming convention
   - Consider tenant isolation for multi-tenant data

## Pagination

### Using PaginationHelper

```csharp
public async Task<PagedResult<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize)
{
    var query = _context.Customers
        .Where(c => c.IsActive);
    
    return await query.ToPagedResultAsync(pageNumber, pageSize);
}
```

### Default Pagination Parameters

- **Default Page Size:** 10 items
- **Maximum Page Size:** 100 items
- **Default Page Number:** 1

### Pagination in Controllers

```csharp
[HttpGet]
public async Task<IActionResult> GetCustomers(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
{
    var result = await _customerService.GetCustomersAsync(pageNumber, pageSize);
    
    var response = ApiResponse<PagedResult<CustomerDto>>.SuccessResponse(result);
    return Ok(response);
}
```

## Query Optimization

### 1. Use Projection for Read-Only Queries

**Good:**
```csharp
var summaries = await _context.Customers
    .Select(c => new CustomerSummaryDto
    {
        Id = c.Id,
        Code = c.Code,
        Name = c.Name
    })
    .ToListAsync();
```

**Avoid:**
```csharp
var customers = await _context.Customers.ToListAsync();
var summaries = customers.Select(c => new CustomerSummaryDto { ... });
```

### 2. Use Compiled Queries for Frequently Used Queries

```csharp
private static readonly Func<TenantDbContext, Guid, Task<Customer?>> GetByIdQuery =
    EF.CompileAsyncQuery((TenantDbContext context, Guid id) =>
        context.Customers.FirstOrDefault(c => c.Id == id));

public async Task<Customer?> GetByIdAsync(Guid id)
{
    return await GetByIdQuery(_context, id);
}
```

### 3. Avoid N+1 Query Problems

**Bad:**
```csharp
var customers = await _context.Customers.ToListAsync();
foreach (var customer in customers)
{
    var contacts = await _context.CustomerContacts
        .Where(cc => cc.CustomerId == customer.Id)
        .ToListAsync(); // N+1 queries!
}
```

**Good:**
```csharp
var customers = await _context.Customers
    .Include(c => c.CustomerContacts)
        .ThenInclude(cc => cc.Contact)
    .ToListAsync(); // Single query with joins
```

### 4. Use AsNoTracking for Read-Only Queries

```csharp
var customers = await _context.Customers
    .AsNoTracking()
    .Where(c => c.IsActive)
    .ToListAsync();
```

### 5. Use Split Queries for Multiple Collections

```csharp
var customer = await _context.Customers
    .Include(c => c.CustomerContacts)
    .Include(c => c.PropertyAssets)
    .AsSplitQuery()
    .FirstOrDefaultAsync(c => c.Id == id);
```

## Database Indexing

### Recommended Indexes

Based on common query patterns:

```csharp
// In Entity Configuration
modelBuilder.Entity<Customer>()
    .HasIndex(c => c.Code)
    .IsUnique();

modelBuilder.Entity<Customer>()
    .HasIndex(c => new { c.TenantId, c.IsActive });

modelBuilder.Entity<CustomerContact>()
    .HasIndex(cc => new { cc.CustomerId, cc.ContactId });
```

### Index Guidelines

1. **Index Foreign Keys** - Frequently used in joins
2. **Index Filtered Columns** - Columns used in WHERE clauses
3. **Index Sorted Columns** - Columns used in ORDER BY
4. **Composite Indexes** - For multi-column queries
5. **Avoid Over-Indexing** - Indexes slow down writes

## Response Compression

Enable response compression for large payloads:

```csharp
services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
```

## Async/Await Best Practices

### Always Use Async for I/O Operations

```csharp
// Good
public async Task<CustomerDto> GetCustomerAsync(Guid id)
{
    return await _repository.GetByIdAsync(id);
}

// Avoid
public CustomerDto GetCustomer(Guid id)
{
    return _repository.GetById(id).Result; // Blocks thread
}
```

### ConfigureAwait(false) for Library Code

```csharp
public async Task<CustomerDto> GetCustomerAsync(Guid id)
{
    var customer = await _repository.GetByIdAsync(id).ConfigureAwait(false);
    return Mapper.Map<CustomerDto>(customer);
}
```

## Performance Monitoring

### Logging Slow Queries

```csharp
public class QueryLoggingInterceptor : DbCommandInterceptor
{
    public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var interceptionResult = await base.ReaderExecutingAsync(
            command, eventData, result, cancellationToken);
        
        stopwatch.Stop();
        
        if (stopwatch.ElapsedMilliseconds > 1000) // Log queries > 1 second
        {
            _logger.LogWarning(
                "Slow query detected: {CommandText} took {ElapsedMs}ms",
                command.CommandText,
                stopwatch.ElapsedMilliseconds);
        }
        
        return interceptionResult;
    }
}
```

### Health Checks

Use health checks to monitor database performance:

```csharp
services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>(
        "core_database",
        tags: new[] { "db", "sql" });
```

## Best Practices Summary

1. **Cache Frequently Accessed Data** - Reduce database load
2. **Use Pagination** - Limit result set sizes
3. **Optimize Queries** - Use projections, compiled queries, proper includes
4. **Index Strategically** - Based on query patterns
5. **Use Async Everywhere** - Don't block threads
6. **Monitor Performance** - Log slow queries, use health checks
7. **Compress Responses** - Reduce network transfer
8. **Avoid Premature Optimization** - Measure first, optimize second

## Migration to Distributed Caching

For production with multiple instances, consider migrating to Redis:

```csharp
// Replace IMemoryCache with IDistributedCache
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
});

services.AddScoped<ICacheService, DistributedCacheService>();
```

