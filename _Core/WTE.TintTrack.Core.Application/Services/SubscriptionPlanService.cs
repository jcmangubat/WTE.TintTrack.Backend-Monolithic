using AutoMapper;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class SubscriptionPlanService(IMapper mapper,
                    ILogger<SubscriptionPlanService> logger,
                    IMessageProviderService messageProviderService,
                    ISubscriptionPlanRepository subscriptionPlanRepository)
    : MappedLoggingService<ISubscriptionPlanService>(mapper, logger, messageProviderService), ISubscriptionPlanService
{
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository = subscriptionPlanRepository;

    public async Task DeleteSubscriptionPlanAsync(Guid id)
    {
        try
        {
            await _subscriptionPlanRepository.DeleteAsync(id);
            await _subscriptionPlanRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync(bool excludeInActives = true)
    {
        try
        {
            var subscriptionPlans = await _subscriptionPlanRepository.GetListAsync(p => !excludeInActives || (excludeInActives && p.IsActive == true));
            var subscriptionPlanDtos = Mapper.Map<IEnumerable<SubscriptionPlanDto>>(subscriptionPlans);
            return subscriptionPlanDtos;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(p => p.Id == id);
            var SubscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
            return SubscriptionPlanDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDto> CreateAsync(SubscriptionPlanDto SubscriptionPlanDto)
    {
        try
        {
            if (string.IsNullOrEmpty(SubscriptionPlanDto.PlanCode))
            {
                var planCode = CodeGenerator.GenerateUniqueCode(SubscriptionPlanDto.Name, FieldLengths.SubscriptionPlan.PlanCode);
                SubscriptionPlanDto.PlanCode = planCode;
            }

            var subscriptionPlan = Mapper.Map<SubscriptionPlan>(SubscriptionPlanDto);

            await _subscriptionPlanRepository.AddAsync(subscriptionPlan);
            await _subscriptionPlanRepository.CommitAsync();

            SubscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
            return SubscriptionPlanDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDto> UpdateAsync(string planCode, SubscriptionPlanDto subscriptionPlanDto)
    {
        try
        {
            var existingSubscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(p => p.PlanCode == planCode)
                ?? throw RecordNotFoundException("ERR056");

            subscriptionPlanDto.Id= existingSubscriptionPlan.Id;

            var subscriptionPlan = Mapper.Map<SubscriptionPlan>(subscriptionPlanDto);

            await _subscriptionPlanRepository.UpdateAsync(subscriptionPlan);
            await _subscriptionPlanRepository.CommitAsync();

            subscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
            return subscriptionPlanDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDto> UpdateAsync(Guid id, SubscriptionPlanDto SubscriptionPlanDto)
    {
        try
        {
            var subscriptionPlan = Mapper.Map<SubscriptionPlan>(SubscriptionPlanDto);

            if (!await _subscriptionPlanRepository.AnyAsync(p => p.Id == SubscriptionPlanDto.Id))
                throw RecordNotFoundException("ERR057");

            await _subscriptionPlanRepository.UpdateAsync(subscriptionPlan);
            await _subscriptionPlanRepository.CommitAsync();

            SubscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
            return SubscriptionPlanDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanDto> GetByPlanCodeAsync(string planCode)
    {
        try
        {
            SubscriptionPlan subscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(sp => sp.PlanCode == planCode)
                ?? throw RecordNotFoundException("ERR056");

            var subscriptionPlanDto = Mapper.Map<SubscriptionPlanDto>(subscriptionPlan);
            return subscriptionPlanDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}

