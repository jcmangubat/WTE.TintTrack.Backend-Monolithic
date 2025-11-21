using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.SalesAndQuoting;

public class WorkOrderRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<WorkOrder>(dbContext), IWorkOrderRepository { }



