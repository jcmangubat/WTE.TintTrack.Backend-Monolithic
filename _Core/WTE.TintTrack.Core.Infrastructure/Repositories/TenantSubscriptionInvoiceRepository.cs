using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class TenantSubscriptionInvoiceRepository(ApplicationDbContext dbContext)
    : RepositoryForGuidKeyedEntity<TenantSubscriptionInvoice>(dbContext), ITenantSubscriptionInvoiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    /*public async Task<IEnumerable<TenantSubscriptionInvoice>> GetByTenantSubscriptionAsync(Guid tenantSubscriptionId)
    {
        return await _dbContext.TenantSubscriptionInvoices
            .Where(p => p.TenantSubscriptionId == tenantSubscriptionId)
            .ToListAsync();
    }*/

    public async Task<TenantSubscriptionInvoice?> GetByIdAsync(Guid invoiceId)
    {
        return await _dbContext.TenantSubscriptionInvoices
            .FirstOrDefaultAsync(p => p.Id == invoiceId);
    }

    public async Task<TenantSubscriptionInvoice?> GetByInvoiceNoAsync(string invoiceNo)
    {
        return await _dbContext.TenantSubscriptionInvoices
            .FirstOrDefaultAsync(p => p.InvoiceNo.ToLower().Equals(invoiceNo.ToLower(), StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task DeleteAsync(Guid invoiceId)
    {
        var entity = await GetByIdAsync(invoiceId);
        if (entity != null)
            _dbContext.TenantSubscriptionInvoices.Remove(entity);
    }
}
