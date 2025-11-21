using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class PropertyService(
    IMapper mapper,
    ILogger<PropertyService> logger,
    IMessageProviderService messageProviderService,
    IPropertyAssetRepository repository)
    : MappedLoggingServiceWithCRUD<IPropertyAssetService, IPropertyAssetRepository, PropertyAsset, PropertyAssetDto>(
        mapper, logger, messageProviderService, repository), IPropertyAssetService
{
    public async Task<PropertyAssetDto?> GetByCodeAsync(string code)
    {
        var entity = await Repository.GetSingleAsync(p => p.Code == code);
        return Mapper.Map<PropertyAssetDto>(entity);
    }
}
