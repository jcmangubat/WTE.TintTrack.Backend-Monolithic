using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Interfaces;
using SMEAppHouse.Core.Patterns.EF.Exceptions;
using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using SMEAppHouse.Core.Patterns.Repo.Paging;
using System.Linq.Expressions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ModelAbstraction;
using WTE.TintTrack.Common.Helpers;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public abstract class MappedLoggingServiceWithCRUD<TService, TRepository, TEntity, TEntityDto>(
        IMapper mapper, ILogger<TService> logger, IMessageProviderService messageProviderService, TRepository entityRepository
) :
    MappedLoggingService<TService>(mapper, logger, messageProviderService),
    IMappedLoggingServiceWithCRUD<TService, TRepository, TEntity, TEntityDto>
    where TService : class
    where TEntity : class, IKeyedEntity<Guid>
    where TEntityDto : class, IEntity
    where TRepository : class, IRepositoryForKeyedEntity<TEntity, Guid>
{
    public TRepository Repository { get; private set; } = entityRepository;

    public async Task<TEntityDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var result = await Repository.GetSingleAsync(p => p.Id == id);
            var resultsDto = Mapper.Map<TEntityDto>(result);
            return resultsDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<TEntityDto> AddAsync(TEntityDto entityDto)
    {
        try
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            await Repository.AddAsync(entity);
            await Repository.CommitAsync();

            entityDto = Mapper.Map<TEntityDto>(entity);
            return entityDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<TEntityDto> UpdateAsync(TEntityDto entityDto)
    {
        try
        {
            var entity = Mapper.Map<TEntity>(entityDto);
            await Repository.UpdateAsync(entity);
            await Repository.CommitAsync();
            entityDto = Mapper.Map<TEntityDto>(entity);
            return entityDto;
        }
        catch (EntityNotFoundException<TEntity> ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntityDto, bool>> dtoPredicate)
    {
        try
        {
            var converter = new PredicateConverter<TEntity, TEntityDto>();
            Expression<Func<TEntity, bool>> entityPredicate = converter.Convert(dtoPredicate);
            return await Repository.AnyAsync(entityPredicate);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<IEnumerable<TEntityDto>> FindByAsync(Expression<Func<TEntityDto, bool>> dtoPredicate)
    {
        try
        {
            var converter = new PredicateConverter<TEntity, TEntityDto>();
            Expression<Func<TEntity, bool>> entityPredicate = converter.Convert(dtoPredicate);
            var items = await Repository.GetListAsync(entityPredicate);
            return Mapper.Map<IEnumerable<TEntityDto>>(items);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<TEntityDto> FindSingleAsync(Expression<Func<TEntityDto, bool>> dtoPredicate, params Expression<Func<TEntityDto, object>>[]? includes)
    {
        try
        {
            IQueryable<TEntityDto>? queryable = null;
            if (includes != null && includes.Length != 0)
                queryable = GetAllAsQueryable(includes);
            else queryable = GetAllAsQueryable();
            var item = await queryable.FirstOrDefaultAsync(dtoPredicate);

            /*var converter = new PredicateConverter<TEntity, TEntityDto>();
            Expression<Func<TEntity, bool>> entityPredicate = converter.Convert(dtoPredicate);

            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePredicate = null;

            // Convert each include expression and combine them into one
            foreach (var include in includes ?? Array.Empty<Expression<Func<TEntityDto, object>>>())
            {
                var entityInclude = ExpressionConverter.ConvertExpression<TEntity, TEntityDto, object>(include);
                includePredicate = includePredicate == null
                    ? (query) => query.Include(entityInclude)
                    : (query) => includePredicate(query).Include(entityInclude);
            }
            var item = await Repository.GetSingleAsync(entityPredicate, includePredicate);*/

            return Mapper.Map<TEntityDto>(item);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while finding a single entity. Predicate: {dtoPredicate}, Includes: {includes}\r\nError message: {ex.GetExceptionMessages()}");
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await Repository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task DeleteAsync(IEnumerable<TEntityDto> entitiesDto)
    {
        try
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(entitiesDto);
            await Repository.DeleteAsync(entities);
            await Repository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            if (!await Repository.AnyAsync(p => p.Id == id))
                return false;

            await Repository.DeleteAsync(p => p.Id == id);
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }

    public async Task<PagedResultForDTO<TEntityDto>> GetAllAsync(PageRequest pageRequest, Expression<Func<TEntityDto, bool>> dtoPredicate)
    {
        try
        {
            var converter = new PredicateConverter<TEntity, TEntityDto>();
            Expression<Func<TEntity, bool>> entityPredicate = converter.Convert(dtoPredicate);
            var result = await Repository.GetListAsync(pageRequest, entityPredicate);

            return new PagedResultForDTO<TEntityDto>
            {
                PageRequest = pageRequest,
                Data = Mapper.Map<IEnumerable<TEntityDto>>(result.Data),
                TotalPages = result.TotalPages,
                TotalRecords = result.TotalRecords
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }



    public IQueryable<TEntityDto> GetAllAsQueryable()
    {
        return Repository.DbContext
                .Set<TEntity>()
                .ProjectTo<TEntityDto>(Mapper.ConfigurationProvider);
    }

    public IQueryable<TEntityDto> GetAllAsQueryable(Expression<Func<TEntityDto, bool>> dtoPredicate)
    {
        return Repository.DbContext
                .Set<TEntity>()
                .ProjectTo<TEntityDto>(Mapper.ConfigurationProvider)
                .Where(dtoPredicate);
    }

    public IQueryable<TEntityDto> GetAllAsQueryable(params Expression<Func<TEntityDto, object>>[] includes)
    {
        try
        {
            // Start with the base query
            var query = Repository.DbContext.Set<TEntity>().AsQueryable();

            // Apply includes if provided
            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    var entityInclude = ExpressionConverter.ConvertExpression<TEntity, TEntityDto, object>(include);
                    query = query.Include(entityInclude);
                }
            }

            // Project to TEntityDto using AutoMapper
            return query.ProjectTo<TEntityDto>(Mapper.ConfigurationProvider);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw new ApplicationException("An error occurred while processing your request.", ex);
        }
    }
}
