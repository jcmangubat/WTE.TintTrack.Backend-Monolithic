using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.WorkOrder;
using WTE.TintTrack.Api.Messaging.Business.Responses.WorkOrder;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling work order operations.
/// </summary>
/// <remarks>
/// This controller manages work orders, which represent tasks or jobs to be performed for customers. It provides CRUD operations for creating, reading, updating, and deleting work order records, enabling businesses to track and manage work assignments, job progress, and service delivery throughout the project lifecycle.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize]
public class WorkOrderController(
    ILogger<WorkOrderController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IWorkOrderService workOrderService,
    ICRUDExtender<WorkOrderDto, CreateWorkOrderRequest, UpdateWorkOrderRequest> entityCRUDExtender,
    IValidator<CreateWorkOrderRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<WorkOrderController, WorkOrderDto, WorkOrderResponse, CreateWorkOrderRequest, UpdateWorkOrderRequest>(
        logger, mapper, messageProviderService, workOrderService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}