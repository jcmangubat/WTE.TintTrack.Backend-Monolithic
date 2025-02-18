using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.CodeKits.Extensions;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class TenantSubscriptionPaymentRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<TenantSubscriptionPayment>(dbContext), ITenantSubscriptionPaymentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<TenantSubscriptionPayment>> GetByTenantSubscriptionAsync(Guid tenantSubscriptionId)
    {
        return await _dbContext.TenantSubscriptionPayments
                        .Include(p => p.TenantSubscriptionInvoice)
                            .Where(p => p.TenantSubscriptionInvoice != null && p.TenantSubscriptionInvoice.TenantSubscriptionId == tenantSubscriptionId)
                        .ToListAsync();
    }

    public async Task<IEnumerable<TenantSubscriptionPayment>> GetByInvoiceNoAsync(string invoiceNo)
    {
        return await _dbContext.TenantSubscriptionPayments
                        .Include(p => p.TenantSubscriptionInvoice)
                            .Where(p => p.TenantSubscriptionInvoice != null && p.TenantSubscriptionInvoice.InvoiceNo.ToLower().EqualsCaseInsensitive(invoiceNo))
                        .ToListAsync();
    }

    public async Task<IEnumerable<TenantSubscriptionPayment>> GetByInvoiceIdAsync(Guid invoiceId)
    {
        return await _dbContext.TenantSubscriptionPayments
                        .Include(p => p.TenantSubscriptionInvoice)
                            .Where(p => p.TenantSubscriptionInvoice != null && p.TenantSubscriptionInvoice.Id == invoiceId)
                        .ToListAsync();
    }

    public async Task<TenantSubscriptionPayment?> GetByIdAsync(Guid paymentId)
    {
        return await _dbContext.TenantSubscriptionPayments
            .FirstOrDefaultAsync(p => p.Id == paymentId);
    }

    public async Task DeleteAsync(Guid paymentId)
    {
        var entity = await GetByIdAsync(paymentId);
        if (entity != null)
            _dbContext.TenantSubscriptionPayments.Remove(entity);
    }
}
