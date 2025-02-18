using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class PropertyRepository(TenantDbContext dbContext) : 
    RepositoryForGuidKeyedEntity<Property>(dbContext), IPropertyRepository { }
