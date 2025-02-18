using SMEAppHouse.Core.Patterns.Repo.Abstractions;
using WTE.TintTrack.Core.Domain.Entities;

namespace WTE.TintTrack.Core.Domain.Interfaces.Repositories;

public interface ITenantSubscriptionPaymentRepository : IRepositoryForKeyedEntity<TenantSubscriptionPayment, Guid>
{
    Task<IEnumerable<TenantSubscriptionPayment>> GetByInvoiceIdAsync(Guid invoiceId);
    Task<IEnumerable<TenantSubscriptionPayment>> GetByInvoiceNoAsync(string invoiceNo);
    Task<IEnumerable<TenantSubscriptionPayment>> GetByTenantSubscriptionAsync(Guid tenantSubscriptionId);
    Task<TenantSubscriptionPayment?> GetByIdAsync(Guid paymentId);
    Task DeleteAsync(Guid paymentId);
}

