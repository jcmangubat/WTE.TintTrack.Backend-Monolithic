using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.SalesAndQuoting;

public class WorkOrderItemRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<WorkOrderItem>(dbContext), IWorkOrderItemRepository { }



