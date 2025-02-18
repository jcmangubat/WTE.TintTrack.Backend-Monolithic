using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProposalController(
    ILogger<ProposalController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    IProposalService proposalService,

    ICRUDExtender<ProposalDto, CreateProposalRequest, UpdateProposalRequest> entityCRUDExtender,
    IValidator<CreateProposalRequest> entityCreateRequestValidator,
    IValidator<UpdateProposalRequest> entityUpdateRequestValidator)
    : CodedEntityOperationsControllerBase<ProposalController, ProposalDto, ProposalResponse, CreateProposalRequest, UpdateProposalRequest>(
            logger, mapper, messageProviderService, proposalService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{
}
