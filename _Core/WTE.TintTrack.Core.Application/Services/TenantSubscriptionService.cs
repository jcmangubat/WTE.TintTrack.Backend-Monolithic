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
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Services;

public class TenantSubscriptionService(IMapper mapper,
                    ILogger<TenantSubscriptionService> logger,
                    IMessageProviderService messageProviderService,
                    ITenantSubscriptionRepository tenantSubscriptionRepository)
    : MappedLoggingService<ITenantSubscriptionService>(mapper, logger, messageProviderService), ITenantSubscriptionService
{
    private readonly ITenantSubscriptionRepository _tenantSubscriptionRepository = tenantSubscriptionRepository;

    public async Task DeleteSubscriptionAsync(Guid subscriptionId)
    {
        try
        {
            await _tenantSubscriptionRepository.DeleteAsync(subscriptionId);
            await _tenantSubscriptionRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionDto?> GetSubscriptionByIdAsync(Guid subscriptionId)
    {
        try
        {
            var tenantSubscription = await _tenantSubscriptionRepository.GetByIdAsync(subscriptionId);
            var tenantSubscriptionDto = Mapper.Map<TenantSubscriptionDto>(tenantSubscription);
            return tenantSubscriptionDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<TenantSubscriptionDto>> GetSubscriptionsByTenantAsync(string tenantCode, string? planCode = null, SubscriptionStatusEnum? subscriptionStatus = SubscriptionStatusEnum.Active)
    {
        try
        {
            var tenantSubscriptions = await _tenantSubscriptionRepository
                                                .GetListAsync(p => p.Tenant.TenantCode == tenantCode &&
                                                                    (planCode == null || (planCode != null && p.SubscriptionPlan.PlanCode == planCode) &&
                                                                    (subscriptionStatus == null || (subscriptionStatus != null && p.SubscriptionStatus == subscriptionStatus))),
                                                            p => p.Include(x => x.Tenant)
                                                                    .Include(x => x.SubscriptionPlan));
            var tenantSubscriptionDtos = Mapper.Map<IEnumerable<TenantSubscriptionDto>>(tenantSubscriptions);
            return tenantSubscriptionDtos;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeactivateActiveSubscriptionAsync(string tenantCode)
    {
        try
        {
            var activeTenantSubscriptionDto = await GetActiveSubscriptionByTenantAsync(tenantCode)
                ?? throw RecordNotFoundException("ERR069");

            var tenantSubscription = Mapper.Map<TenantSubscription>(activeTenantSubscriptionDto);
            tenantSubscription.SubscriptionStatus = SubscriptionStatusEnum.Inactive;
            tenantSubscription.DateModified = DateTime.UtcNow;
            tenantSubscription.DateArchived = DateTime.UtcNow;
            tenantSubscription.ReasonArchived = "plan replacement";
            await _tenantSubscriptionRepository.UpdateAsync(tenantSubscription);
            await _tenantSubscriptionRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionDto> GetActiveSubscriptionByTenantAsync(string tenantCode)
    {
        try
        {
            var tenantSubscription = await _tenantSubscriptionRepository
                                                .GetSingleAsync(p => p.Tenant.TenantCode == tenantCode && p.SubscriptionStatus == SubscriptionStatusEnum.Active,
                                                            p => p.Include(x => x.Tenant).Include(x => x.SubscriptionPlan))
                                                ?? throw RecordNotFoundException("ERR069");

            var tenantSubscriptionDto = Mapper.Map<TenantSubscriptionDto>(tenantSubscription);
            return tenantSubscriptionDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<TenantSubscriptionDto> RegisterTenantSubscriptionAsync(TenantSubscriptionDto tenantSubscriptionDto)
    {
        try
        {
            if (tenantSubscriptionDto.Id == Guid.Empty)
                tenantSubscriptionDto.Id = Guid.NewGuid();

            var tenantSubscription = Mapper.Map<TenantSubscription>(tenantSubscriptionDto);
            await _tenantSubscriptionRepository.AddAsync(tenantSubscription);
            await _tenantSubscriptionRepository.CommitAsync();

            return Mapper.Map<TenantSubscriptionDto>(tenantSubscription);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeleteSubscriptionAsync(string tenantCode, string planCode)
    {
        try
        {
            var tenantSubscription = await _tenantSubscriptionRepository
                                                .GetSingleAsync(p => p.Tenant.TenantCode == tenantCode &&
                                                                        p.SubscriptionPlan.PlanCode == planCode,
                                                            p => p.Include(x => x.Tenant).Include(x => x.SubscriptionPlan))
                                                ?? throw RecordNotFoundException("ERR069");


            await _tenantSubscriptionRepository.DeleteAsync(tenantSubscription);
            await _tenantSubscriptionRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}