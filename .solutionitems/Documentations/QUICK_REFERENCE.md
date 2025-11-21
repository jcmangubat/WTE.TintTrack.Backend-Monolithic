# Quick Reference Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Common Patterns and Usage

### Unit of Work Pattern

```csharp
// Inject Unit of Work
public class CustomerService
{
    private readonly ITenantUnitOfWork _unitOfWork;
    
    public CustomerService(ITenantUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // Simple operation
    public async Task CreateCustomerAsync(CustomerDto dto)
    {
        var customer = Mapper.Map<Customer>(dto);
        await _customerRepository.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync(); // Auto-dispatches domain events
    }
    
    // Transaction operation
    public async Task CreateCustomerWithAddressAsync(CustomerDto customerDto, AddressDto addressDto)
    {
        await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var customer = Mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);
            
            var address = Mapper.Map<Address>(addressDto);
            address.CustomerId = customer.Id;
            await _addressRepository.AddAsync(address);
        });
    }
}
```

### Result Pattern

```csharp
// In Service
public async Task<Result<CustomerDto>> GetCustomerAsync(Guid id)
{
    var customer = await _repository.GetByIdAsync(id);
    if (customer == null)
        return Result<CustomerDto>.NotFound("Customer not found");
    
    return Result<CustomerDto>.Success(Mapper.Map<CustomerDto>(customer));
}

// In Controller
[HttpGet("{id}")]
public async Task<IActionResult> GetCustomer(Guid id)
{
    var result = await _customerService.GetCustomerAsync(id);
    
    if (result.IsSuccess)
        return CreateStandardizedResponse(result.Value);
    
    var statusCode = result.GetHttpStatusCode();
    return CreateErrorResponse(result.ErrorMessage, result.ErrorCode, result.Errors, statusCode);
}
```

### Domain Events

```csharp
// Create Event
public class CustomerCreatedEvent : DomainEventBase
{
    public Guid CustomerId { get; }
    public string CustomerCode { get; }
    
    public CustomerCreatedEvent(Guid customerId, string customerCode)
    {
        CustomerId = customerId;
        CustomerCode = customerCode;
    }
}

// Raise Event in Entity
public class Customer : EntityBase
{
    public static Customer Create(string code, string name)
    {
        var customer = new Customer { Code = code, Name = name };
        customer.AddDomainEvent(new CustomerCreatedEvent(customer.Id, code));
        return customer;
    }
}

// Handle Event
public class CustomerCreatedEventHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    public async Task HandleAsync(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // Audit logging, notifications, cache invalidation, etc.
        await _auditLogService.LogAsync(...);
    }
}

// Register Handler
services.AddScoped<IDomainEventHandler<CustomerCreatedEvent>, CustomerCreatedEventHandler>();
```

### Pagination

```csharp
// In Repository/Service
public async Task<PagedResult<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize)
{
    var query = _context.Customers.Where(c => c.IsActive);
    return await query.ToPagedResultAsync(pageNumber, pageSize);
}

// In Controller
[HttpGet]
public async Task<IActionResult> GetCustomers(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
{
    var result = await _customerService.GetCustomersAsync(pageNumber, pageSize);
    return CreateStandardizedResponse(result);
}
```

### Caching

```csharp
// Get or Set with Cache
public async Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync()
{
    return await _cacheService.GetOrSetAsync(
        "subscription_plans",
        async () => await _subscriptionPlanRepository.GetAllAsync(),
        TimeSpan.FromHours(1)
    );
}

// Invalidate Cache
public async Task UpdateAsync(Guid id, SubscriptionPlanDto dto)
{
    await _subscriptionPlanRepository.UpdateAsync(id, dto);
    await _cacheService.RemoveAsync("subscription_plans");
}
```

### Tenant Context

```csharp
// Access Tenant Context
public class CustomerService
{
    private readonly ITenantContext _tenantContext;
    
    public CustomerService(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }
    
    public async Task CreateCustomerAsync(CustomerDto dto)
    {
        _tenantContext.EnsureResolved(); // Throws if not resolved
        var tenantId = _tenantContext.TenantId!.Value;
        
        var customer = Mapper.Map<Customer>(dto);
        customer.TenantId = tenantId;
        // ...
    }
}
```

### Standardized API Response

```csharp
// Success Response
return CreateStandardizedResponse(data, "Operation successful", StatusCodes.Status200OK);

// Error Response
return CreateErrorResponse("Error message", "ERROR_CODE", errors, StatusCodes.Status400BadRequest);
```

### Configuration Access

```csharp
// Inject Strongly-Typed Configuration
public class MyService
{
    private readonly ApplicationSettings _appSettings;
    private readonly JwtSettings _jwtSettings;
    
    public MyService(
        ApplicationSettings appSettings,
        JwtSettings jwtSettings)
    {
        _appSettings = appSettings;
        _jwtSettings = jwtSettings;
    }
}
```

## Common Extension Methods

### Result Extensions

```csharp
// Map result
var nameResult = customerResult.Map(c => c.Name);

// Chain operations
var result = await GetCustomerAsync(id)
    .Bind(customer => UpdateCustomerAsync(customer))
    .OnSuccess(updated => _logger.LogInformation("Updated: {Id}", updated.Id))
    .OnFailure(error => _logger.LogError("Error: {Error}", error));

// Combine results
var combined = ResultExtensions.Combine(result1, result2, result3);
```

### Unit of Work Extensions

```csharp
// Execute with automatic transaction management
var result = await _unitOfWork.ExecuteInTransactionWithResultAsync(async () =>
{
    // Operations
    return Result<T>.Success(data);
});
```

## Error Codes Reference

- `NOT_FOUND` - Resource not found (404)
- `VALIDATION_ERROR` - Validation failed (400)
- `UNAUTHORIZED` - Authentication required (401)
- `FORBIDDEN` - Insufficient permissions (403)
- `CONFLICT` - Resource conflict (409)

## Best Practices Checklist

### Service Layer
- [ ] Use Unit of Work for transactions
- [ ] Return Result<T> for operations that can fail
- [ ] Raise domain events for side effects
- [ ] Use caching for frequently accessed data
- [ ] Validate input before processing

### Repository Layer
- [ ] Don't expose IQueryable directly
- [ ] Use async methods consistently
- [ ] Return nullable types when appropriate
- [ ] Include related entities explicitly
- [ ] Use projection for read-only queries

### Controller Layer
- [ ] Use standardized API responses
- [ ] Include correlation IDs
- [ ] Handle Result<T> properly
- [ ] Validate requests with FluentValidation
- [ ] Use appropriate HTTP status codes

### Domain Layer
- [ ] Raise domain events for significant actions
- [ ] Keep business logic in entities
- [ ] Use value objects for complex types
- [ ] Keep entities focused and cohesive

## Common Issues and Solutions

### Issue: Domain Events Not Firing
**Solution:** Ensure entity inherits from `EntityBase` and implements `IHasDomainEvents`

### Issue: Tenant Context Not Resolved
**Solution:** Ensure `TenantContextMiddleware` is registered before controllers

### Issue: Cache Not Invalidating
**Solution:** Invalidate cache in service methods that modify data

### Issue: Transaction Not Committing
**Solution:** Use `ExecuteInTransactionAsync` or call `SaveChangesAsync` explicitly

### Issue: Result Pattern Not Working
**Solution:** Ensure using `Result<T>.Success()` or `Result<T>.Failure()` factory methods

## Quick Links

- [Transaction Management](TRANSACTION_MANAGEMENT.md)
- [Repository Pattern](REPOSITORY_PATTERN.md)
- [Domain Events](DOMAIN_EVENTS.md)
- [Performance Optimization](PERFORMANCE_OPTIMIZATION.md)
- [Configuration Management](CONFIGURATION_MANAGEMENT.md)
- [Testing Infrastructure](TESTING_INFRASTRUCTURE.md)

