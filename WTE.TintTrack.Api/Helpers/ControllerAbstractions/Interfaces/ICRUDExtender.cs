using System.Linq.Expressions;
using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;

public interface ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest>
    where TEntityDto : class
    where TEntityCreateRequest : class, IEntityCreateRequest
    where TEntityUpdateRequest : class, IEntityUpdateRequest
{
    TEntityDto TransformForUpdate(TEntityDto entityDto, TEntityUpdateRequest entityUpdateRequest);
    Task<bool> ExistAsync(TEntityDto entity);
    Task<(bool Success, TEntityDto? createdEntity)> ExecuteAlternativeAsync(TEntityCreateRequest createEntityRequest);
    Expression<Func<TEntityDto, object>>[]? GetIncludes();
}