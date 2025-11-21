using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.TintMaterial;
using WTE.TintTrack.Api.Messaging.Business.Responses.TintMaterial;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.TintMaterialModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling tint material price history operations.
/// </summary>
/// <remarks>
/// This controller manages historical pricing data for tint materials, tracking price changes over time. It provides operations for creating and retrieving price history records, enabling businesses to maintain an audit trail of pricing changes and analyze pricing trends for materials used in their operations.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TintMaterialPriceHistoryController(
    ILogger<TintMaterialPriceHistoryController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    ITintMaterialPriceHistoryService tintMaterialPriceHistoryService,
    ICRUDExtender<TintMaterialPriceHistoryDto, CreateTintMaterialPriceHistoryRequest, UpdateTintMaterialPriceHistoryRequest> entityCRUDExtender,
    IValidator<CreateTintMaterialPriceHistoryRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<TintMaterialPriceHistoryController, TintMaterialPriceHistoryDto, TintMaterialPriceHistoryResponse, CreateTintMaterialPriceHistoryRequest, UpdateTintMaterialPriceHistoryRequest>(
        logger, mapper, messageProviderService, tintMaterialPriceHistoryService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}