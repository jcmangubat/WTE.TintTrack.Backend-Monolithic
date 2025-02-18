using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using WTE.TintTrack.Business.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories;

public class CustomerRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<Customer>(dbContext), ICustomerRepository
{

}
