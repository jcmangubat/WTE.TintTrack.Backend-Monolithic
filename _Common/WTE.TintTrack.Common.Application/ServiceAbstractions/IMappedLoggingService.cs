using AutoMapper;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public interface IMappedLoggingService<TService> : ILoggingService<TService>
     where TService : class
{
    IMapper Mapper { get; }
}
