using WTE.TintTrack.Business.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class PropertyAssetRepository(TenantDbContext dbContext) : 
    RepositoryForGuidKeyedEntity<PropertyAsset>(dbContext), IPropertyAssetRepository { }
