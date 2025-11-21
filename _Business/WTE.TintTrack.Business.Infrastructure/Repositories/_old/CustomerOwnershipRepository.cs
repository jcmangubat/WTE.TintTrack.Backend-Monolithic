using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class CustomerOwnershipRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<CustomerOwnership>(dbContext), ICustomerOwnershipRepository { }
