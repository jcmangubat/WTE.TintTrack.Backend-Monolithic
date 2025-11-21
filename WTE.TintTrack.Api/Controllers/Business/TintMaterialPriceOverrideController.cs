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
/// Controller for handling tint material price override operations.
/// </summary>
/// <remarks>
/// This controller manages price overrides for tint materials, allowing businesses to set custom pricing that differs from standard tier pricing. It provides operations for creating, reading, updating, and deleting price override records, enabling flexible pricing strategies for specific customers, projects, or special circumstances.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TintMaterialPriceOverrideController(
    ILogger<TintMaterialPriceOverrideController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    ITintMaterialPriceOverrideService tintMaterialPriceOverrideService,
    ICRUDExtender<TintMaterialPriceOverrideDto, CreateTintMaterialPriceOverrideRequest, UpdateTintMaterialPriceOverrideRequest> entityCRUDExtender,
    IValidator<CreateTintMaterialPriceOverrideRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<TintMaterialPriceOverrideController, TintMaterialPriceOverrideDto, TintMaterialPriceOverrideResponse, CreateTintMaterialPriceOverrideRequest, UpdateTintMaterialPriceOverrideRequest>(
        logger, mapper, messageProviderService, tintMaterialPriceOverrideService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}
