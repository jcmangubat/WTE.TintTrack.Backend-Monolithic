using WTE.TintTrack.Common.Events;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Common.Models;

/// <summary>
/// Base class for entities that can raise domain events
/// </summary>
public abstract class EntityBase : IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (domainEvent == null)
            return;

        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

