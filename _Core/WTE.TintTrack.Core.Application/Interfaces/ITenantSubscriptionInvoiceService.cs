using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface ITenantSubscriptionInvoiceService : IMappedLoggingService<ITenantSubscriptionInvoiceService>
{
    Task<TenantSubscriptionInvoiceDto?> GetLatestInvoiceAsync(string tenantCode);
    Task<IEnumerable<TenantSubscriptionInvoiceDto>> GetInvoicesByTenantSubscriptionAsync(string tenantCode, string planCode);
    Task<TenantSubscriptionInvoiceDto?> GetInvoiceByIdAsync(Guid invoiceId);
    Task<TenantSubscriptionInvoiceDto?> GetInvoiceByInvoiceNoAsync(string invoiceNo);
    Task<bool> AnyByInvoiceNoAsync(string invoiceNo);
    Task<bool> AnyByInvoiceCodeAsync(string invoiceCode);

    Task AddInvoiceAsync(TenantSubscriptionInvoiceDto invoice);
    Task UpdateInvoiceAsync(TenantSubscriptionInvoiceDto invoice);
    Task DeleteInvoiceAsync(string invoiceNo);
}
