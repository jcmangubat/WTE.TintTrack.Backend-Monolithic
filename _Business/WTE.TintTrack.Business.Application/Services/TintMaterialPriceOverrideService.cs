using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Application.Services;

public class TintMaterialPriceOverrideService(
    IMapper mapper,
    ILogger<TintMaterialPriceOverrideService> logger,
    IMessageProviderService messageProviderService,
    ITintMaterialPriceOverrideRepository repository)
    : MappedLoggingServiceWithCRUD<ITintMaterialPriceOverrideService, ITintMaterialPriceOverrideRepository, TintMaterialPriceOverride, TintMaterialPriceOverrideDto>(mapper, logger, messageProviderService, repository),
                            ITintMaterialPriceOverrideService
{

}
