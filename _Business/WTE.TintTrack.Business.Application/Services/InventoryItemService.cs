using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

public class InventoryItemService(
    IMapper mapper,
    ILogger<InventoryItemService> logger,
    IMessageProviderService messageProviderService,
    IInventoryItemRepository repository)
    : MappedLoggingServiceWithCRUD<IInventoryItemService, IInventoryItemRepository, InventoryItem, InventoryItemDto>(mapper, logger, messageProviderService, repository),
                            IInventoryItemService
{
    
}
