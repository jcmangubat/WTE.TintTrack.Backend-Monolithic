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
/// Controller for handling tint material price tier operations.
/// </summary>
/// <remarks>
/// This controller manages price tiers for tint materials, which define different pricing levels based on quantity, customer type, or other criteria. It provides operations for creating, reading, updating, and deleting price tier records, enabling businesses to implement volume-based or tiered pricing strategies for their materials.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TintMaterialPriceTierController(
    ILogger<TintMaterialPriceTierController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    ITintMaterialPriceTierService tintMaterialPriceTierService,
    ICRUDExtender<TintMaterialPriceTierDto, CreateTintMaterialPriceTierRequest, UpdateTintMaterialPriceTierRequest> entityCRUDExtender,
    IValidator<CreateTintMaterialPriceTierRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<TintMaterialPriceTierController, TintMaterialPriceTierDto, TintMaterialPriceTierResponse, CreateTintMaterialPriceTierRequest, UpdateTintMaterialPriceTierRequest>(
        logger, mapper, messageProviderService, tintMaterialPriceTierService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}