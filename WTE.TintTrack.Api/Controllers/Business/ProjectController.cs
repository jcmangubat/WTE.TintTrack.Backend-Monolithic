using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectController(
    ILogger<ProjectController> logger,
    IMapper mapper,
    IProjectService projectService)
    : CodedEntityOperationsControllerBase<ProjectController, ProjectDto>(logger, mapper, projectService)
{
}
