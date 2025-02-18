using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Application.Services;

public class SubscriptionPlanFeatureService(IMapper mapper,
                    ILogger<SubscriptionPlanFeatureService> logger,
                    IMessageProviderService messageProviderService,
                    ISubscriptionPlanRepository subscriptionPlanRepository,
                    ISubscriptionPlanFeatureRepository subscriptionPlanFeatureRepository,
                    ISubscriptionPlanFeatureAssociationRepository subscriptionPlanFeatureAssociationRepository)
    : MappedLoggingService<ISubscriptionPlanFeatureService>(mapper, logger, messageProviderService), ISubscriptionPlanFeatureService
{
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository = subscriptionPlanRepository;
    private readonly ISubscriptionPlanFeatureRepository _subscriptionPlanFeatureRepository = subscriptionPlanFeatureRepository;
    private readonly ISubscriptionPlanFeatureAssociationRepository _subscriptionPlanFeatureAssocRepository = subscriptionPlanFeatureAssociationRepository;

    public async Task DeleteFeatureAsync(Guid planFeatureId)
    {
        try
        {
            if (!await _subscriptionPlanFeatureRepository.AnyAsync(p => p.Id == planFeatureId))
                throw RecordNotFoundException("ERR049");

            await _subscriptionPlanFeatureRepository.DeleteAsync(planFeatureId);
            await _subscriptionPlanFeatureRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeleteFeatureAsync(string planFeatureCode)
    {
        try
        {
            if (!await _subscriptionPlanFeatureRepository.AnyAsync(p => p.FeatureCode == planFeatureCode))
                throw RecordNotFoundException("ERR050");

            await _subscriptionPlanFeatureRepository.DeleteAsync(p => p.FeatureCode == planFeatureCode);
            await _subscriptionPlanFeatureRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanFeatureDto?> GetSubscriptionPlanFeatureAsync(Guid featureId)
    {
        try
        {
            var subscriptionPlanFeature = await _subscriptionPlanFeatureRepository.GetSingleAsync(p => p.Id == featureId)
                                            ?? throw RecordNotFoundException("ERR051");

            var subscriptionPlanFeatureDto = Mapper.Map<SubscriptionPlanFeatureDto>(subscriptionPlanFeature);
            return subscriptionPlanFeatureDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanFeatureDto?> GetSubscriptionPlanFeatureAsync(string planFeatureCode)
    {
        try
        {
            var subscriptionPlanFeature = await _subscriptionPlanFeatureRepository.GetSingleAsync(p => p.FeatureCode == planFeatureCode)
                                            ?? throw RecordNotFoundException("ERR052");

            var subscriptionPlanFeatureDto = Mapper.Map<SubscriptionPlanFeatureDto>(subscriptionPlanFeature);
            return subscriptionPlanFeatureDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<SubscriptionPlanFeatureDto>> GetFeaturesBySubscriptionPlanAsync(string planCode)
    {
        try
        {
            var subscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(p => p.PlanCode == planCode,
                                                            p => p.Include(x => x.SubscriptionPlanFeatureAssociations)
                                                                    .ThenInclude(x => x.SubscriptionPlanFeature))
                ?? throw RecordNotFoundException("ERR053");

            var featuresDto = subscriptionPlan.SubscriptionPlanFeatureAssociations
                                    .Select(p => Mapper.Map<SubscriptionPlanFeatureDto>(p.SubscriptionPlanFeature));

            return featuresDto;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task RemoveFeatureFromPlan(string planCode, string featureCode)
    {
        try
        {
            var subscriptionPlanFeatureAssoc = await _subscriptionPlanFeatureAssocRepository.GetSingleAsync(
                                                            p => p.SubscriptionPlan.PlanCode == planCode &&
                                                                    p.SubscriptionPlanFeature.FeatureCode == featureCode,
                                                            p => p.Include(x => x.SubscriptionPlan).Include(p => p.SubscriptionPlanFeature))
                ?? throw RecordNotFoundException("ERR054");

            await _subscriptionPlanFeatureAssocRepository.DeleteAsync(subscriptionPlanFeatureAssoc);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<SubscriptionPlanFeatureAssociationDto> FindPlanFeatureAssociation(string planCode, string featureCode)
    {
        try
        {
            var subscriptionPlanFeatureAssoc = await _subscriptionPlanFeatureAssocRepository.GetSingleAsync(
                                                            p => p.SubscriptionPlan.PlanCode == planCode &&
                                                                    p.SubscriptionPlanFeature.FeatureCode == featureCode,
                                                            p => p.Include(x => x.SubscriptionPlan).Include(p => p.SubscriptionPlanFeature))
                ?? throw RecordNotFoundException("ERR054");
            
            return Mapper.Map<SubscriptionPlanFeatureAssociationDto>(subscriptionPlanFeatureAssoc);
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task AddFeatureToPlan(string planCode, string featureCode)
    {
        try
        {
            var subscriptionPlanFeatureAssoc = await _subscriptionPlanFeatureAssocRepository.GetSingleAsync(
                                                            p => p.SubscriptionPlan.PlanCode == planCode &&
                                                                    p.SubscriptionPlanFeature.FeatureCode == featureCode,
                                                            p => p.Include(x => x.SubscriptionPlan).Include(p => p.SubscriptionPlanFeature))
                                            ?? throw RecordNotFoundException("ERR055");

            var subscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(p => p.PlanCode == planCode)
                                    ?? throw RecordNotFoundException("ERR056");

            var subscriptionPlanFeature = await _subscriptionPlanFeatureRepository.GetSingleAsync(p => p.FeatureCode == featureCode)
                                            ?? throw RecordNotFoundException("ERR057");
            
            subscriptionPlanFeatureAssoc = new SubscriptionPlanFeatureAssociation()
            {
                SubscriptionPlanFeatureId = subscriptionPlanFeature.Id,
                SubscriptionPlanId = subscriptionPlan.Id
            };

            await _subscriptionPlanFeatureAssocRepository.AddAsync(subscriptionPlanFeatureAssoc);
            await _subscriptionPlanFeatureAssocRepository.CommitAsync();
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}