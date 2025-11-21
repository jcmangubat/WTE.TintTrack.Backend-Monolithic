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
/// Controller for handling tint material management operations.
/// </summary>
/// <remarks>
/// This controller provides comprehensive management of tint materials, including CRUD operations for material records. It integrates with price history, price override, price schedule, and price tier services to provide a complete material management solution, enabling businesses to track materials, manage pricing structures, and maintain material catalogs.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TintMaterialController(
    ILogger<TintMaterialController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    ITintMaterialService tintMaterialService,
    ITintMaterialPriceHistoryService tintMaterialPriceHistoryService,
    ITintMaterialPriceOverrideService tintMaterialPriceOverrideService,
    ITintMaterialPriceScheduleService tintMaterialPriceScheduleService,
    ITintMaterialPriceTierService tintMaterialPriceTierService,

    ICRUDExtender<TintMaterialDto, CreateTintMaterialRequest, UpdateTintMaterialRequest> entityCRUDExtender,
    IValidator<CreateTintMaterialRequest> entityCreateRequestValidator,
    IValidator<UpdateTintMaterialRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<TintMaterialController, TintMaterialDto, TintMaterialResponse, CreateTintMaterialRequest, UpdateTintMaterialRequest>(
            logger, mapper, messageProviderService, tintMaterialService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}