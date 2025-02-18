using AutoMapper;
using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Interfaces;
using System.Linq.Expressions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;

namespace WTE.TintTrack.Api.Messaging._Abstractions
{
    public class CRUDExtenderBase<TRepository, TEntityDto, TEntityCreateRequest, TEntityUpdateRequest>(
            ILogger<ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest>> logger,
            IMapper mapper,
            TRepository repository
        )
        : ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest>
        where TEntityDto : class, IAuditableEntity, IEntity
        where TEntityCreateRequest : class, IEntityCreateRequest
        where TEntityUpdateRequest : class, IApiMessageRequest, IEntityUpdateRequest
    {
        protected readonly ILogger<ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest>> _logger = logger;
        protected readonly IMapper _mapper = mapper;
        protected readonly TRepository _repository = repository;

        public virtual async Task<(bool Success, TEntityDto? createdEntity)> ExecuteAlternativeAsync(TEntityCreateRequest createEntityRequest)
            => await Task.Run<(bool Success, TEntityDto? createdEntity)>(() => (false, null));

        public virtual async Task<bool> ExistAsync(TEntityDto entity)
            => await Task.Run(() => false);

        public virtual Expression<Func<TEntityDto, object>>[]? GetIncludes()
        {
            return null;
        }

        public virtual TEntityDto TransformForUpdate(TEntityDto entityDto, TEntityUpdateRequest entityUpdateRequest)
        {
            entityDto.DateModified = DateTime.UtcNow;

            if (entityUpdateRequest.IsActive != null) entityDto.IsActive = entityUpdateRequest.IsActive;
            if (entityUpdateRequest.IsArchived != null) entityDto.IsArchived = entityUpdateRequest.IsArchived;
            if (entityUpdateRequest.ReasonArchived != null) entityDto.ReasonArchived = entityUpdateRequest.ReasonArchived;

            return entityDto;
        }
    }
}
