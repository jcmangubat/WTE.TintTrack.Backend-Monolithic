namespace WTE.TintTrack.Common.Events;

/// <summary>
/// Handler interface for domain events
/// </summary>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}

