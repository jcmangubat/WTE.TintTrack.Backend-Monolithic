using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.TintMaterialRepos;

public class TintMaterialPriceHistoryRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<TintMaterialPriceHistory>(dbContext), ITintMaterialPriceHistoryRepository { }
