using WTE.TintTrack.Common.Events;

namespace WTE.TintTrack.Common.Interfaces;

/// <summary>
/// Interface for entities that can raise domain events
/// </summary>
public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}

