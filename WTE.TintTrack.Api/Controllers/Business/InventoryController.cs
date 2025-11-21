using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inventory;
using WTE.TintTrack.Api.Messaging.Business.Responses.Inventory;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling inventory item management operations.
/// </summary>
/// <remarks>
/// This controller provides operations for managing inventory items, including creating, reading, updating, and deleting inventory records. It supports tracking stock levels, managing product information, and maintaining inventory data for business operations and supply chain management.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class InventoryController(
    ILogger<InventoryController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IInventoryItemService InventoryItemService,
    ICRUDExtender<InventoryItemDto, CreateInventoryItemRequest, UpdateInventoryItemRequest> entityCRUDExtender,
    IValidator<CreateInventoryItemRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<InventoryController, InventoryItemDto, InventoryItemResponse, CreateInventoryItemRequest, UpdateInventoryItemRequest>(
        logger, mapper, messageProviderService, InventoryItemService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}
