using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Estimate;
using WTE.TintTrack.Api.Messaging.Business.Requests.WorkOrder;
using WTE.TintTrack.Api.Messaging.Business.Responses.Estimate;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling estimate operations.
/// </summary>
/// <remarks>
/// This controller manages estimates, which are preliminary cost assessments for potential projects or services. It provides CRUD operations for creating, reading, updating, and deleting estimates, allowing businesses to prepare and manage pricing information before converting estimates into quotes or proposals.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EstimateController(
    ILogger<EstimateController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IEstimateService EstimateService,

    ICRUDExtender<EstimateDto, CreateEstimateRequest, UpdateWorkOrderItemRequest> entityCRUDExtender,
    IValidator<CreateEstimateRequest> entityCreateRequestValidator,
    IValidator<UpdateWorkOrderItemRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<EstimateController, EstimateDto, EstimateResponse, CreateEstimateRequest, UpdateWorkOrderItemRequest>(
            logger, mapper, messageProviderService, EstimateService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}
