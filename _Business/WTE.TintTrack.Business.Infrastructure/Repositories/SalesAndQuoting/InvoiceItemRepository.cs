using SMEAppHouse.Core.Patterns.Repo;

namespace WTE.TintTrack.Business.Infrastructure.Repositories.SalesAndQuoting;

public class InvoiceItemRepository(TenantDbContext dbContext) : RepositoryForGuidKeyedEntity<InvoiceItem>(dbContext), IInvoiceItemRepository { }



