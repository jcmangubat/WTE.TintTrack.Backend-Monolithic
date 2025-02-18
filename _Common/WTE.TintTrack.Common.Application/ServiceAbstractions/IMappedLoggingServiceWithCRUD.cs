using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Interfaces;
using SMEAppHouse.Core.Patterns.Repo.Abstractions;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

// Composite service interface that includes CRUD operations with logging and mapping
public interface IMappedLoggingServiceWithCRUD<TService, TRepository, TEntity, TEntityDto>
    : IMappedLoggingService<TService>, ICRUDService<TEntityDto>
    where TService : class
    where TEntity : class, IKeyedEntity<Guid>
    where TEntityDto : class
    where TRepository : class, IRepositoryForKeyedEntity<TEntity, Guid>
{
    TRepository Repository { get; }
}