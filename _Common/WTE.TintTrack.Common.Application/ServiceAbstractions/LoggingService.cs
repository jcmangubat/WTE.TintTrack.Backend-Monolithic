using Microsoft.Extensions.Logging;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public class LoggingService<TService>(ILogger<TService> logger)
    : ILoggingService<TService>
    where TService : class
{
    public ILogger<TService> Logger { get; } = logger ?? throw new ArgumentNullException(nameof(logger));
}
