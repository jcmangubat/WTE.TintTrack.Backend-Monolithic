using Microsoft.EntityFrameworkCore;
using WTE.TintTrack.Common.Events;
using WTE.TintTrack.Common.Interfaces;

namespace WTE.TintTrack.Common.Extensions;

/// <summary>
/// Extension methods for domain events
/// </summary>
public static class DomainEventExtensions
{
    /// <summary>
    /// Publishes domain events from entities in the change tracker
    /// </summary>
    public static async Task PublishDomainEventsAsync(
        this DbContext context,
        IDomainEventDispatcher dispatcher,
        CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear events before dispatching to avoid duplicate processing
        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        await dispatcher.DispatchAsync(domainEvents, cancellationToken).ConfigureAwait(false);
    }
}

