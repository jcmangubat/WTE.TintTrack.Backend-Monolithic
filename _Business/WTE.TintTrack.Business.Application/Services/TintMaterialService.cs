using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities.TintMaterialEntities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories.TintMaterialRepos;

namespace WTE.TintTrack.Business.Application.Services;

public class TintMaterialService(
    IMapper mapper,
    ILogger<TintMaterialService> logger,
    IMessageProviderService messageProviderService,
    ITintMaterialRepository repository)
    : MappedLoggingServiceWithCRUD<ITintMaterialService, ITintMaterialRepository, TintMaterial, TintMaterialDto>(mapper, logger, messageProviderService, repository),
                            ITintMaterialService
{
    public async Task<TintMaterialDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<TintMaterialDto>(entity);
    }
}
