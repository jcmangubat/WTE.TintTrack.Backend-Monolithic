using SMEAppHouse.Core.Patterns.Repo.Paging;
using System.Linq.Expressions;
using WTE.TintTrack.Application.Shared.ModelAbstraction;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

// CRUD service interface with basic data operations
public interface ICRUDService<TEntityDto>
    where TEntityDto : class 
{
    /// <summary>
    /// Retrieve an entity by its unique identifier.
    /// </summary>
    Task<TEntityDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Add a new entity.
    /// </summary>
    Task<TEntityDto> AddAsync(TEntityDto entityDto);

    /// <summary>
    /// Update an existing entity.
    /// </summary>
    Task<TEntityDto> UpdateAsync(TEntityDto entityDto);

    /// <summary>
    /// Remove an entity by its unique identifier.
    /// </summary>
    Task<bool> DeleteAsync(Guid id);

    Task DeleteAsync(IEnumerable<TEntityDto> entitiesDto);

    /// <summary>
    /// Check if an entity exists by its unique identifier.
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<TEntityDto, bool>> dtoPredicate);

    /// <summary>
    /// Retrieve entities matching the specified filter criteria.
    /// </summary>
    Task<IEnumerable<TEntityDto>> FindByAsync(Expression<Func<TEntityDto, bool>> dtoPredicate);

    Task<TEntityDto> FindSingleAsync(Expression<Func<TEntityDto, bool>> dtoPredicate, params Expression<Func<TEntityDto, object>>[]? includes);

    /// <summary>
    /// Save any pending changes to the underlying storage (optional for Unit of Work pattern).
    /// </summary>
    Task SaveChangesAsync();

    Task<PagedResultForDTO<TEntityDto>> GetAllAsync(PageRequest pageRequest, Expression<Func<TEntityDto, bool>> dtoPredicate);
    IQueryable<TEntityDto> GetAllAsQueryable();
    IQueryable<TEntityDto> GetAllAsQueryable(Expression<Func<TEntityDto, bool>> dtoPredicate);
    IQueryable<TEntityDto> GetAllAsQueryable(params Expression<Func<TEntityDto, object>>[] includes);
}