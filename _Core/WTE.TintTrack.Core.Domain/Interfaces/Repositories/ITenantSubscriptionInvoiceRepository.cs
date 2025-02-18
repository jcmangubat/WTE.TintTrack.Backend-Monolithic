using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ITenantSubscriptionInvoiceRepository
    : IRepositoryForKeyedEntity<TenantSubscriptionInvoice, Guid>
{

    //Task<IEnumerable<TenantSubscriptionInvoice>> GetByTenantSubscriptionAsync(Guid tenantSubscriptionId);
    Task<TenantSubscriptionInvoice?> GetByIdAsync(Guid invoiceId);
    Task<TenantSubscriptionInvoice?> GetByInvoiceNoAsync(string invoiceNo);
}

