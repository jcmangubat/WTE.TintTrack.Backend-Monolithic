# Domain Events Pattern Guide

📖 **Documentation Index:** [Return to Solution Items README](../README.md)

---

## Overview

This document describes the Domain Events pattern implementation in the TintTrack application. Domain events allow you to decouple side effects (like audit logging, notifications, cache invalidation) from your core business logic.

## What are Domain Events?

Domain events represent something important that happened in your domain. They are raised by entities when significant business events occur, and handlers can react to these events without the entity needing to know about those handlers.

## Benefits

1. **Decoupling** - Entities don't need to know about side effects
2. **Testability** - Easier to test business logic without side effects
3. **Extensibility** - Easy to add new handlers without modifying existing code
4. **Audit Trail** - Natural way to track what happened in the system

## Architecture

### Core Components

1. **IDomainEvent** - Marker interface for all domain events
2. **DomainEventBase** - Base class with common properties (OccurredOn, EventId)
3. **IDomainEventHandler<T>** - Interface for event handlers
4. **IDomainEventDispatcher** - Dispatches events to registered handlers
5. **IHasDomainEvents** - Interface for entities that can raise events

## Usage Example

### 1. Create a Domain Event

```csharp
using WTE.TintTrack.Common.Events;

namespace WTE.TintTrack.Business.Domain.Events;

public class CustomerCreatedEvent : DomainEventBase
{
    public Guid CustomerId { get; }
    public string CustomerCode { get; }
    public string CustomerName { get; }
    public Guid TenantId { get; }

    public CustomerCreatedEvent(Guid customerId, string customerCode, string customerName, Guid tenantId)
    {
        CustomerId = customerId;
        CustomerCode = customerCode;
        CustomerName = customerName;
        TenantId = tenantId;
    }
}
```

### 2. Raise Event from Entity

```csharp
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Business.Domain.Events;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Customer : EntityBase, ICodedEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public static Customer Create(string code, string name)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Code = code,
            Name = name
        };

        // Raise domain event
        customer.AddDomainEvent(new CustomerCreatedEvent(
            customer.Id,
            customer.Code,
            customer.Name,
            customer.TenantId));

        return customer;
    }
}
```

### 3. Create Event Handler

```csharp
using WTE.TintTrack.Common.Events;
using WTE.TintTrack.Business.Domain.Events;

namespace WTE.TintTrack.Business.Application.Handlers;

public class CustomerCreatedEventHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    private readonly IAuditLogService _auditLogService;
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(
        IAuditLogService auditLogService,
        ILogger<CustomerCreatedEventHandler> logger)
    {
        _auditLogService = auditLogService;
        _logger = logger;
    }

    public async Task HandleAsync(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Log the event
        _logger.LogInformation(
            "Customer created: {CustomerId} - {CustomerCode} - {CustomerName}",
            domainEvent.CustomerId,
            domainEvent.CustomerCode,
            domainEvent.CustomerName);

        // Create audit log entry
        await _auditLogService.LogAsync(new AuditLogDto
        {
            EntityType = "Customer",
            EntityId = domainEvent.CustomerId,
            Action = "Created",
            Description = $"Customer {domainEvent.CustomerCode} was created"
        });
    }
}
```

### 4. Dispatch Events in Unit of Work

```csharp
public class TenantUnitOfWork : ITenantUnitOfWork
{
    private readonly TenantDbContext _context;
    private readonly IDomainEventDispatcher _eventDispatcher;

    public TenantUnitOfWork(
        TenantDbContext context,
        IDomainEventDispatcher eventDispatcher)
    {
        _context = context;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events before saving
        await _context.PublishDomainEventsAsync(_eventDispatcher, cancellationToken);
        
        // Save changes
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
```

### 5. Register Handlers in DI

```csharp
// In DIExtension.cs or BusinessServiceRegistration.cs
services.AddScoped<IDomainEventHandler<CustomerCreatedEvent>, CustomerCreatedEventHandler>();
services.AddScoped<IDomainEventHandler<CustomerUpdatedEvent>, CustomerUpdatedEventHandler>();
services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
```

## Best Practices

### 1. Keep Events Immutable

```csharp
// Good: Immutable event
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

// Avoid: Mutable event
public class CustomerCreatedEvent : DomainEventBase
{
    public Guid CustomerId { get; set; } // Mutable
    public string CustomerCode { get; set; } // Mutable
}
```

### 2. Include Only Necessary Data

```csharp
// Good: Only essential data
public class CustomerCreatedEvent : DomainEventBase
{
    public Guid CustomerId { get; }
    public string CustomerCode { get; }
}

// Avoid: Including entire entity
public class CustomerCreatedEvent : DomainEventBase
{
    public Customer Customer { get; } // Too much data
}
```

### 3. Use Descriptive Event Names

```csharp
// Good: Clear and descriptive
CustomerCreatedEvent
CustomerUpdatedEvent
CustomerDeletedEvent
OrderPlacedEvent
OrderShippedEvent

// Avoid: Vague names
CustomerEvent
OrderEvent
```

### 4. Handle Failures Gracefully

```csharp
public class CustomerCreatedEventHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    public async Task HandleAsync(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        try
        {
            // Handle event
        }
        catch (Exception ex)
        {
            // Log error but don't throw - other handlers should still execute
            _logger.LogError(ex, "Error handling CustomerCreatedEvent");
            // Consider sending to dead letter queue for retry
        }
    }
}
```

### 5. Don't Use Events for Business Logic

```csharp
// Bad: Using events for business logic
public class Order
{
    public void Place()
    {
        // Business logic in event handler
        AddDomainEvent(new OrderPlacedEvent(this));
    }
}

// Good: Business logic in entity, events for side effects
public class Order
{
    public void Place()
    {
        // Business logic here
        Status = OrderStatus.Placed;
        // Event for side effects (notifications, audit, etc.)
        AddDomainEvent(new OrderPlacedEvent(Id, CustomerId));
    }
}
```

## Common Use Cases

### 1. Audit Logging

```csharp
public class AuditLoggingHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    public async Task HandleAsync(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        await _auditLogService.LogAsync(new AuditLogDto
        {
            EntityType = "Customer",
            EntityId = domainEvent.CustomerId,
            Action = "Created"
        });
    }
}
```

### 2. Cache Invalidation

```csharp
public class CacheInvalidationHandler : IDomainEventHandler<CustomerUpdatedEvent>
{
    public async Task HandleAsync(CustomerUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync($"customer_{domainEvent.CustomerId}");
        await _cacheService.RemoveByPatternAsync("customers_*");
    }
}
```

### 3. Notifications

```csharp
public class NotificationHandler : IDomainEventHandler<OrderPlacedEvent>
{
    public async Task HandleAsync(OrderPlacedEvent domainEvent, CancellationToken cancellationToken)
    {
        await _emailService.SendAsync(new EmailDto
        {
            To = domainEvent.CustomerEmail,
            Subject = "Order Confirmation",
            Body = $"Your order {domainEvent.OrderId} has been placed."
        });
    }
}
```

### 4. Integration Events

```csharp
public class IntegrationEventHandler : IDomainEventHandler<CustomerCreatedEvent>
{
    public async Task HandleAsync(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        // Publish to message queue for other services
        await _messageBus.PublishAsync(new CustomerCreatedIntegrationEvent
        {
            CustomerId = domainEvent.CustomerId,
            CustomerCode = domainEvent.CustomerCode
        });
    }
}
```

## Testing

### Testing Event Raising

```csharp
[Test]
public void CreateCustomer_RaisesCustomerCreatedEvent()
{
    // Arrange
    var customer = Customer.Create("C001", "Test Customer");

    // Act
    var events = customer.DomainEvents;

    // Assert
    Assert.AreEqual(1, events.Count);
    Assert.IsInstanceOf<CustomerCreatedEvent>(events.First());
}
```

### Testing Event Handlers

```csharp
[Test]
public async Task HandleAsync_LogsAuditEntry()
{
    // Arrange
    var auditService = new Mock<IAuditLogService>();
    var handler = new CustomerCreatedEventHandler(auditService.Object, _logger);
    var @event = new CustomerCreatedEvent(Guid.NewGuid(), "C001", "Test");

    // Act
    await handler.HandleAsync(@event);

    // Assert
    auditService.Verify(s => s.LogAsync(It.IsAny<AuditLogDto>()), Times.Once);
}
```

## Migration Guide

To migrate existing code to use domain events:

1. **Identify side effects** - Find code that does more than core business logic
2. **Create events** - Define domain events for significant actions
3. **Raise events** - Add event raising to entity methods
4. **Create handlers** - Move side effect code to handlers
5. **Register handlers** - Add handler registrations to DI
6. **Update Unit of Work** - Add event dispatching to SaveChangesAsync

## Performance Considerations

- Events are dispatched synchronously by default
- For high-volume scenarios, consider async event processing
- Use message queues for cross-service events
- Batch events when possible to reduce overhead

