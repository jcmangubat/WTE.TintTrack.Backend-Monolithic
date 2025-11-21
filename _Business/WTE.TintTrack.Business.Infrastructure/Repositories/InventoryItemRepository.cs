using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class InventoryItemRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<InventoryItem>(dbContext), IInventoryItemRepository { }
