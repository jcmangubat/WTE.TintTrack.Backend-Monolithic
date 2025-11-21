using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;

namespace WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

public interface ITintMaterialPriceTierRepository : IRepositoryForKeyedEntity<TintMaterialPriceTier, Guid>
{
}