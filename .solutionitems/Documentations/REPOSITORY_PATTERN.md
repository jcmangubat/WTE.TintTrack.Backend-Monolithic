# Repository Pattern Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the repository pattern implementation in the TintTrack application and best practices for using repositories.

## Repository Structure

### Base Interfaces

Repositories inherit from base interfaces that provide common CRUD operations:

- `IRepositoryForKeyedEntity<TEntity, TKey>` - For entities with a key (Guid, int, etc.)
- Provides: `GetByIdAsync`, `GetListAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`, `CommitAsync`

### Domain-Specific Interfaces

Each domain entity has its own repository interface that extends the base interface:

```csharp
public interface ICustomerRepository : IRepositoryForKeyedEntity<Customer, Guid>
{
    Task<Customer?> GetByCodeAsync(string code);
    Task<IEnumerable<Customer>> GetByTenantAsync(Guid tenantId);
    // Domain-specific methods
}
```

## Best Practices

### 1. Use Repository Pattern, Not Direct DbContext Access

**Good:**
```csharp
public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;
    
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<CustomerDto> GetCustomerAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return Mapper.Map<CustomerDto>(customer);
    }
}
```

**Avoid:**
```csharp
public class CustomerService
{
    private readonly TenantDbContext _context; // Direct DbContext access
    
    public async Task<CustomerDto> GetCustomerAsync(Guid id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        return Mapper.Map<CustomerDto>(customer);
    }
}
```

### 2. Don't Expose IQueryable Directly

**Good:**
```csharp
public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<IEnumerable<Customer>> GetByTenantAsync(Guid tenantId);
}
```

**Avoid:**
```csharp
public interface ICustomerRepository
{
    IQueryable<Customer> GetAll(); // Exposes queryable, breaks encapsulation
}
```

### 3. Use Specifications for Complex Queries

For complex queries, consider using the Specification pattern:

```csharp
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
}

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> FindAsync(ISpecification<Customer> spec);
}
```

### 4. Use Unit of Work for Transactions

**Good:**
```csharp
public async Task CreateCustomerWithAddressAsync(CustomerDto customerDto, AddressDto addressDto)
{
    await _unitOfWork.ExecuteInTransactionAsync(async () =>
    {
        var customer = Mapper.Map<Customer>(customerDto);
        await _customerRepository.AddAsync(customer);
        
        var address = Mapper.Map<Address>(addressDto);
        address.CustomerId = customer.Id;
        await _addressRepository.AddAsync(address);
        
        // Single SaveChanges call
    });
}
```

**Avoid:**
```csharp
public async Task CreateCustomerWithAddressAsync(CustomerDto customerDto, AddressDto addressDto)
{
    await _customerRepository.AddAsync(customer);
    await _customerRepository.CommitAsync(); // Multiple commits
    
    await _addressRepository.AddAsync(address);
    await _addressRepository.CommitAsync();
}
```

### 5. Include Related Entities Explicitly

**Good:**
```csharp
public interface ICustomerRepository
{
    Task<Customer?> GetByIdWithContactsAsync(Guid id);
    Task<Customer?> GetByIdWithAddressesAsync(Guid id);
}
```

**Avoid:**
```csharp
// Loading all related entities by default
public async Task<Customer?> GetByIdAsync(Guid id)
{
    return await _context.Customers
        .Include(c => c.Contacts)
        .Include(c => c.Addresses)
        .Include(c => c.PropertyAssets)
        // ... all includes
        .FirstOrDefaultAsync(c => c.Id == id);
}
```

### 6. Use Async Methods Consistently

All repository methods should be async:

```csharp
public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Customer customer);
}
```

### 7. Handle Null Returns Appropriately

Repository methods should return nullable types when appropriate:

```csharp
// Good: Returns nullable when entity might not exist
Task<Customer?> GetByIdAsync(Guid id);

// Good: Returns empty collection, not null
Task<IEnumerable<Customer>> GetAllAsync();
```

## Repository Implementation Example

```csharp
public class CustomerRepository : ICustomerRepository
{
    private readonly TenantDbContext _context;
    
    public CustomerRepository(TenantDbContext context)
    {
        _context = context;
    }
    
    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Customer?> GetByCodeAsync(string code)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Code == code);
    }
    
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .ToListAsync();
    }
    
    public async Task AddAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer);
    }
    
    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);
    }
    
    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
```

## Performance Considerations

### 1. Use Projection for Read-Only Queries

```csharp
public async Task<IEnumerable<CustomerSummaryDto>> GetCustomerSummariesAsync()
{
    return await _context.Customers
        .Select(c => new CustomerSummaryDto
        {
            Id = c.Id,
            Code = c.Code,
            Name = c.Name
        })
        .ToListAsync();
}
```

### 2. Use Pagination for Large Result Sets

```csharp
public async Task<(IEnumerable<Customer> Items, int TotalCount)> GetPagedAsync(
    int pageNumber, 
    int pageSize)
{
    var query = _context.Customers;
    var totalCount = await query.CountAsync();
    
    var items = await query
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    return (items, totalCount);
}
```

### 3. Use Compiled Queries for Frequently Used Queries

```csharp
private static readonly Func<TenantDbContext, Guid, Task<Customer?>> GetByIdQuery =
    EF.CompileAsyncQuery((TenantDbContext context, Guid id) =>
        context.Customers.FirstOrDefault(c => c.Id == id));

public async Task<Customer?> GetByIdAsync(Guid id)
{
    return await GetByIdQuery(_context, id);
}
```

## Testing Repositories

### In-Memory Database for Testing

```csharp
public class CustomerRepositoryTests
{
    private TenantDbContext _context;
    private ICustomerRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        
        _context = new TenantDbContext(options);
        _repository = new CustomerRepository(_context);
    }
    
    [Test]
    public async Task GetByIdAsync_ReturnsCustomer_WhenExists()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Code = "C001", Name = "Test" };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _repository.GetByIdAsync(customer.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(customer.Id, result.Id);
    }
}
```

## Common Patterns

### 1. Filtering

```csharp
public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
{
    return await _context.Customers
        .Where(c => c.IsActive)
        .ToListAsync();
}
```

### 2. Sorting

```csharp
public async Task<IEnumerable<Customer>> GetAllSortedByNameAsync()
{
    return await _context.Customers
        .OrderBy(c => c.Name)
        .ToListAsync();
}
```

### 3. Including Related Entities

```csharp
public async Task<Customer?> GetByIdWithContactsAsync(Guid id)
{
    return await _context.Customers
        .Include(c => c.CustomerContacts)
            .ThenInclude(cc => cc.Contact)
        .FirstOrDefaultAsync(c => c.Id == id);
}
```

## Migration Guide

When migrating from direct DbContext access to repositories:

1. **Create repository interface** - Define methods needed by services
2. **Implement repository** - Move DbContext logic to repository
3. **Update services** - Inject repository instead of DbContext
4. **Update tests** - Mock repository instead of DbContext
5. **Remove direct DbContext access** - Ensure no services access DbContext directly

