using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class TenantSubscriptionInvoiceService(IMapper mapper,
                                    ILogger<TenantSubscriptionInvoiceService> logger,
                                    IMessageProviderService messageProviderService,
                                    ITenantSubscriptionInvoiceRepository invoiceRepository)
    : MappedLoggingService<ITenantSubscriptionInvoiceService>(mapper, logger, messageProviderService), ITenantSubscriptionInvoiceService
{
    private readonly ITenantSubscriptionInvoiceRepository _invoiceRepository = invoiceRepository
        ?? throw new ArgumentNullException(nameof(invoiceRepository));

    public async Task<TenantSubscriptionInvoiceDto?> GetLatestInvoiceAsync(string tenantCode)
    {
        var latestInvoice = await _invoiceRepository.DbSet
                                        .Include(p => p.TenantSubscription).ThenInclude(p => p.Tenant)
                                        .Where(p => p.TenantSubscription.Tenant.TenantCode == tenantCode)
                                        .OrderByDescending(i => i.DateCreated)  // Order by the most recent invoice date
                                        .FirstOrDefaultAsync();                 // Get the first (latest) invoice
        var latestInvoiceDto = Mapper.Map<TenantSubscriptionInvoiceDto>(latestInvoice);
        return latestInvoiceDto;
    }

    public async Task<IEnumerable<TenantSubscriptionInvoiceDto>> GetInvoicesByTenantSubscriptionAsync(string tenantCode, string planCode)
    {
        try
        {
            // Retrieve invoices by tenant subscription
            var invoices = await _invoiceRepository.GetListAsync(p => p.TenantSubscription.Tenant.TenantCode == tenantCode && p.TenantSubscription.SubscriptionPlan.PlanCode == planCode,
                                                                    p => p.Include(x => x.TenantSubscription).ThenInclude(x => x.Tenant),
                                                                    p => p.OrderBy(x => x.DateCreated));
            var invoicesDto = Mapper.Map<List<TenantSubscriptionInvoiceDto>>(invoices);
            return invoicesDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionInvoiceDto?> GetInvoiceByIdAsync(Guid invoiceId)
    {
        try
        {
            // Retrieve an invoice by its ID
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            var invoiceDto = Mapper.Map<TenantSubscriptionInvoiceDto>(invoice);
            return invoiceDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionInvoiceDto?> GetInvoiceByInvoiceNoAsync(string invoiceNo)
    {
        try
        {
            // Retrieve an invoice by its invoice number
            var invoice = await _invoiceRepository.GetByInvoiceNoAsync(invoiceNo);
            var invoiceDto = Mapper.Map<TenantSubscriptionInvoiceDto>(invoice);
            return invoiceDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task AddInvoiceAsync(TenantSubscriptionInvoiceDto invoiceDto)
    {
        try
        {
            // Validate the invoice (e.g., check for duplicates)
            if (await _invoiceRepository.GetByInvoiceNoAsync(invoiceDto.InvoiceNo) != null)
                throw CustomInvalidOperationException("ERR067", new() { { "{{InvoiceNo}}", invoiceDto.InvoiceNo } });

            // Add the invoice using the repository
            var invoice = Mapper.Map<TenantSubscriptionInvoice>(invoiceDto);
            await _invoiceRepository.AddAsync(invoice);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task UpdateInvoiceAsync(TenantSubscriptionInvoiceDto invoiceDto)
    {
        try
        {
            // Validate the invoice exists before updating
            var existingInvoice = await _invoiceRepository.GetByIdAsync(invoiceDto.Id)
                ?? throw CustomInvalidOperationException("ERR068", new() { { "{{InvoiceId}}", invoiceDto.Id.ToString() } });

            // Update the invoice using the repository
            var invoice = Mapper.Map<TenantSubscriptionInvoice>(invoiceDto);
            await _invoiceRepository.UpdateAsync(invoice);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeleteInvoiceAsync(string invoiceNo)
    {
        try
        {
            // Validate the invoice exists before deleting
            var existingInvoice = await _invoiceRepository.GetSingleAsync(p => p.InvoiceNo == invoiceNo)
                ?? throw CustomInvalidOperationException("ERR067", new() { { "{{InvoiceNo}}", invoiceNo } });

            // Delete the invoice using the repository
            await _invoiceRepository.DeleteAsync(existingInvoice);
            await _invoiceRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<bool> AnyByInvoiceNoAsync(string invoiceNo)
        => await _invoiceRepository.AnyAsync(p => p.InvoiceNo == invoiceNo);

    public async Task<bool> AnyByInvoiceCodeAsync(string invoiceCode)
        => await _invoiceRepository.AnyAsync(p => p.InvoiceCode == invoiceCode);
}