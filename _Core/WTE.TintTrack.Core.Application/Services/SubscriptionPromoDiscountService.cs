using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class SubscriptionPlanDiscountService(IMapper mapper,
                    ILogger<SubscriptionPlanDiscountService> logger,
                    IMessageProviderService messageProviderService,
                    ISubscriptionPlanDiscountRepository subscriptionPlanDiscountRepository)
    : MappedLoggingService<ISubscriptionPlanDiscountService>(mapper, logger, messageProviderService), ISubscriptionPlanDiscountService
{
    private readonly ISubscriptionPlanDiscountRepository _subscriptionPlanDiscountRepository = subscriptionPlanDiscountRepository;

    public async Task DeleteAsync(string planDiscountCode)
    {
        try
        {
            if (!await _subscriptionPlanDiscountRepository.AnyAsync(p => p.PlanDiscountCode == planDiscountCode))
                throw RecordNotFoundException("ERR058");
            
            await _subscriptionPlanDiscountRepository.DeleteAsync(p => p.PlanDiscountCode == planDiscountCode);
            await _subscriptionPlanDiscountRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDiscountDto?> GetByIdAsync(Guid discountId)
    {
        try
        {
            var subscriptionPlanDiscount = await _subscriptionPlanDiscountRepository.GetByIdAsync(discountId);
            var subscriptionPlanDiscountDto = Mapper.Map<SubscriptionPlanDiscountDto>(subscriptionPlanDiscount);
            return subscriptionPlanDiscountDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<SubscriptionPlanDiscountDto>> GetByPlanCodeAsync(string planCode)
    {
        try
        {
            var subscriptionPlanDiscount = await _subscriptionPlanDiscountRepository
                                                .GetListAsync(p => p.SubscriptionPlan.PlanCode == planCode,
                                                                p => p.Include(x => x.SubscriptionPlan));
            var subscriptionPlanDiscountDto = Mapper.Map<IEnumerable<SubscriptionPlanDiscountDto>>(subscriptionPlanDiscount);
            return subscriptionPlanDiscountDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDiscountDto> GetByPlanDiscountCodeAsync(string planDiscountCode)
    {
        try
        {
            var subscriptionPlanDiscount = await _subscriptionPlanDiscountRepository
                                                .GetSingleAsync(p => p.PlanDiscountCode == planDiscountCode,
                                                                p => p.Include(x => x.SubscriptionPlan));
            var subscriptionPlanDiscountDto = Mapper.Map<SubscriptionPlanDiscountDto>(subscriptionPlanDiscount);
            return subscriptionPlanDiscountDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<SubscriptionPlanDiscountDto>> GetBySubscriptionPlanAsync(string planCode)
    {
        try
        {
            var subscriptionPlanDiscounts = await _subscriptionPlanDiscountRepository
                                                    .GetListAsync(p => p.SubscriptionPlan.PlanCode == planCode,
                                                                    p => p.Include(x => x.SubscriptionPlan));

            var subscriptionPlanDiscountDtos = Mapper.Map<List<SubscriptionPlanDiscountDto>>(subscriptionPlanDiscounts);

            return subscriptionPlanDiscountDtos;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}