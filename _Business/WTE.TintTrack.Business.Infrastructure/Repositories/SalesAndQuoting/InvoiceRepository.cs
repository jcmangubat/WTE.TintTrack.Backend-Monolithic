using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.SalesAndQuoting;

public class InvoiceRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<Invoice>(dbContext), IInvoiceRepository { }



