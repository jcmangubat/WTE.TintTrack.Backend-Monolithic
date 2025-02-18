using AutoMapper;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class TenantSubscriptionPaymentService(IMapper mapper,
                                    ILogger<TenantSubscriptionPaymentService> logger,
                                    IMessageProviderService messageProviderService,
                                    ITenantSubscriptionPaymentRepository tenantSubscriptionPaymentRepository)
    : MappedLoggingService<ITenantSubscriptionPaymentService>(mapper, logger, messageProviderService), ITenantSubscriptionPaymentService
{
    private readonly ITenantSubscriptionPaymentRepository _tenantSubscriptionPaymentRepository = tenantSubscriptionPaymentRepository
        ?? throw new ArgumentNullException(nameof(tenantSubscriptionPaymentRepository));

    public async Task DeletePaymentAsync(Guid paymentId)
    {
        try
        {
            await _tenantSubscriptionPaymentRepository.DeleteAsync(paymentId);
            await _tenantSubscriptionPaymentRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionPaymentDto?> GetPaymentByIdAsync(Guid paymentId)
    {
        try
        {
            var tenantSubscriptionPayment = await _tenantSubscriptionPaymentRepository.GetSingleAsync(p => p.Id == paymentId);
            var tenantSubscriptionPaymentDto = Mapper.Map<TenantSubscriptionPaymentDto>(tenantSubscriptionPayment);
            return tenantSubscriptionPaymentDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<TenantSubscriptionPaymentDto>> GetPaymentsByTenantSubscriptionAsync(Guid tenantSubscriptionId)
    {
        try
        {
            var tenantSubscriptionPayments = await _tenantSubscriptionPaymentRepository.GetByTenantSubscriptionAsync(tenantSubscriptionId);
            var tenantSubscriptionPaymentDtos = Mapper.Map<List<TenantSubscriptionPaymentDto>>(tenantSubscriptionPayments);
            return tenantSubscriptionPaymentDtos;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}