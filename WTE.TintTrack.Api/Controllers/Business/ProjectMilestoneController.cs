using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.Proposal;
using WTE.TintTrack.Api.Messaging.Business.Responses.Proposal;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling project milestone operations.
/// </summary>
/// <remarks>
/// This controller manages project milestones, which represent key checkpoints or deliverables in a project timeline. It provides operations for creating, reading, updating, and deleting milestone records, enabling businesses to track project progress, manage deliverables, and monitor key project phases and completion stages.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectMilestoneController(
    ILogger<ProjectMilestoneController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IProjectMilestoneService ProjectMilesoneService,
    ICRUDExtender<ProjectMilestoneDto, CreateProjectMilesoneRequest, UpdateProjectMilesoneRequest> entityCRUDExtender,
    IValidator<CreateProjectMilesoneRequest> entityCreateRequestValidator)
    : EntityOperationsControllerBase<ProjectMilestoneController, ProjectMilestoneDto, ProjectMilestoneResponse, CreateProjectMilesoneRequest, UpdateProjectMilesoneRequest>(
        logger, mapper, messageProviderService, ProjectMilesoneService, entityCreateRequestValidator, entityCRUDExtender
    )
{
}
