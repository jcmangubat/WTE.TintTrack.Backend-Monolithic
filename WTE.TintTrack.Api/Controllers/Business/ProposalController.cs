using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Proposal;
using WTE.TintTrack.Api.Messaging.Business.Requests.WorkOrder;
using WTE.TintTrack.Api.Messaging.Business.Responses.Proposal;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.CommercialOffersModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling proposal operations.
/// </summary>
/// <remarks>
/// This controller manages proposals, which are formal offers or bids submitted to potential customers. It provides CRUD operations for creating, reading, updating, and deleting proposals, enabling businesses to prepare, manage, and track formal business proposals throughout the sales process.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProposalController(
    ILogger<ProposalController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IProposalService proposalService,

    ICRUDExtender<ProposalDto, CreateProposalRequest, UpdateWorkOrderItemRequest> entityCRUDExtender,
    IValidator<CreateProposalRequest> entityCreateRequestValidator,
    IValidator<UpdateWorkOrderItemRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<ProposalController, ProposalDto, ProposalResponse, CreateProposalRequest, UpdateWorkOrderItemRequest>(
            logger, mapper, messageProviderService, proposalService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}
