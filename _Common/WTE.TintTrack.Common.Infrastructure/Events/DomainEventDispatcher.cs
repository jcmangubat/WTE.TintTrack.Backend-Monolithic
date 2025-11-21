using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Common.Events;

namespace WTE.TintTrack.Common.Infrastructure.Events;

/// <summary>
/// Implementation of domain event dispatcher using dependency injection
/// </summary>
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(
        IServiceProvider serviceProvider,
        ILogger<DomainEventDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        if (domainEvent == null)
            return;

        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            try
            {
                var handleMethod = handlerType.GetMethod("HandleAsync");
                if (handleMethod != null)
                {
                    var task = (Task?)handleMethod.Invoke(handler, new object[] { domainEvent, cancellationToken });
                    if (task != null)
                        await task.ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Error handling domain event {EventType} with handler {HandlerType}",
                    eventType.Name,
                    handler.GetType().Name);
                // Continue with other handlers even if one fails
            }
        }
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            await DispatchAsync(domainEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}

