using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Application.Services;

public class TintMaterialPriceScheduleService(
    IMapper mapper,
    ILogger<TintMaterialPriceScheduleService> logger,
    IMessageProviderService messageProviderService,
    ITintMaterialPriceScheduleRepository repository)
    : MappedLoggingServiceWithCRUD<ITintMaterialPriceScheduleService, ITintMaterialPriceScheduleRepository, TintMaterialPriceSchedule, TintMaterialPriceScheduleDto>(mapper, logger, messageProviderService, repository),
                            ITintMaterialPriceScheduleService
{

}
