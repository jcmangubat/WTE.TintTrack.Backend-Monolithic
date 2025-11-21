# Transaction Management Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes how to use the Unit of Work pattern for transaction management in the TintTrack application.

## Unit of Work Pattern

The application implements the Unit of Work pattern to manage database transactions across multiple operations. This ensures data consistency and allows for atomic operations.

## Available Unit of Work Implementations

### 1. ApplicationUnitOfWork (Core/Platform Database)

Used for operations on the core platform database (shared across tenants):
- User management
- Tenant management
- Subscription management
- Authentication tokens

**Interface:** `IApplicationUnitOfWork`
**Implementation:** `ApplicationUnitOfWork`

### 2. TenantUnitOfWork (Business/Tenant Database)

Used for operations on tenant-specific business databases:
- Customer management
- Property assets
- Inquiries
- Tint materials
- All tenant-specific business data

**Interface:** `ITenantUnitOfWork`
**Implementation:** `TenantUnitOfWork`

## Usage Examples

### Basic Transaction Management

```csharp
public class CustomerService : ICustomerService
{
    private readonly ITenantUnitOfWork _unitOfWork;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ITenantUnitOfWork unitOfWork, ICustomerRepository customerRepository)
    {
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
    {
        // Start a transaction
        using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var customer = Mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customer);
            
            // Save changes within transaction
            await _unitOfWork.SaveChangesAsync();
            
            // Commit transaction
            await _unitOfWork.CommitTransactionAsync();
            
            return Mapper.Map<CustomerDto>(customer);
        }
        catch
        {
            // Rollback on error
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
```

### Using ExecuteInTransactionAsync Helper

The Unit of Work provides helper methods for automatic transaction management:

```csharp
public async Task<Result<CustomerDto>> CreateCustomerWithContactAsync(
    CustomerDto customerDto, 
    ContactDto contactDto)
{
    return await _unitOfWork.ExecuteInTransactionAsync(async () =>
    {
        // Create customer
        var customer = Mapper.Map<Customer>(customerDto);
        await _customerRepository.AddAsync(customer);
        
        // Create contact
        var contact = Mapper.Map<Contact>(contactDto);
        await _contactRepository.AddAsync(contact);
        
        // Link customer and contact
        var customerContact = new CustomerContact
        {
            CustomerId = customer.Id,
            ContactId = contact.Id
        };
        await _customerContactRepository.AddAsync(customerContact);
        
        // Save all changes
        await _unitOfWork.SaveChangesAsync();
        
        return Result<CustomerDto>.Success(Mapper.Map<CustomerDto>(customer));
    });
}
```

### Cross-Database Transactions

When operations span both Core and Tenant databases, handle them separately:

```csharp
public async Task RegisterTenantWithInitialDataAsync(TenantDto tenantDto, CustomerDto initialCustomer)
{
    // Core database transaction
    using var coreTransaction = await _applicationUnitOfWork.BeginTransactionAsync();
    
    try
    {
        // Create tenant in core database
        var tenant = Mapper.Map<Tenant>(tenantDto);
        await _tenantRepository.AddAsync(tenant);
        await _applicationUnitOfWork.SaveChangesAsync();
        await _applicationUnitOfWork.CommitTransactionAsync();
        
        // Tenant database transaction (after tenant is created)
        using var tenantTransaction = await _tenantUnitOfWork.BeginTransactionAsync();
        
        try
        {
            // Create initial customer in tenant database
            var customer = Mapper.Map<Customer>(initialCustomer);
            customer.TenantId = tenant.Id;
            await _customerRepository.AddAsync(customer);
            await _tenantUnitOfWork.SaveChangesAsync();
            await _tenantUnitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _tenantUnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    catch
    {
        await _applicationUnitOfWork.RollbackTransactionAsync();
        throw;
    }
}
```

## Best Practices

1. **Always use transactions for multi-step operations** that must succeed or fail together
2. **Use ExecuteInTransactionAsync** for automatic rollback on exceptions
3. **Keep transactions short** - don't hold transactions open during external API calls
4. **Handle exceptions properly** - always rollback on error
5. **Use appropriate Unit of Work** - ApplicationUnitOfWork for core data, TenantUnitOfWork for tenant data
6. **Don't mix Unit of Work instances** - use the same instance throughout a single operation

## Service Registration

Both Unit of Work implementations are registered as scoped services in `DIExtension.cs`:

```csharp
services.AddScoped<IApplicationUnitOfWork, Core.Infrastructure.UnitOfWork>();
services.AddScoped<ITenantUnitOfWork, Business.Infrastructure.TenantUnitOfWork>();
```

This ensures each HTTP request gets its own Unit of Work instance, which is automatically disposed at the end of the request.

