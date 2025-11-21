namespace WTE.TintTrack.Common.Events;

/// <summary>
/// Base class for domain events
/// </summary>
public abstract class DomainEventBase : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Guid EventId { get; } = Guid.NewGuid();
}

