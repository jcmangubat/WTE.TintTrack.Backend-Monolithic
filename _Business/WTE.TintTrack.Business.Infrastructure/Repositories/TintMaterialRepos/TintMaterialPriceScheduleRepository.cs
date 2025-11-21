using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.TintMaterialRepos;

public class TintMaterialPriceScheduleRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<TintMaterialPriceSchedule>(dbContext), ITintMaterialPriceScheduleRepository { }
