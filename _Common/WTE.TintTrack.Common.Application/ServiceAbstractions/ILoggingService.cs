using Microsoft.Extensions.Logging;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public interface ILoggingService<TService>
    where TService : class
{
    ILogger<TService> Logger { get; }
}
