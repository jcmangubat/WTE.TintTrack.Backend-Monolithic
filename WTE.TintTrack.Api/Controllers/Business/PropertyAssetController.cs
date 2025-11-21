using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo.Paging;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;
using WTE.TintTrack.Api.Messaging.Business.Responses.PropertyAsset;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;
using WTE.TintTrack.Business.Application.Interfaces;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Business;

/// <summary>
/// Controller for handling property asset management operations.
/// </summary>
/// <remarks>
/// This controller provides comprehensive management of property assets, supporting multiple property types including architectural, automotive, commercial, custom, energy-efficient, glass film, outdoor, residential, signage, specialty, and other property types. It provides CRUD operations for creating, reading, updating, and deleting property records, with specialized handling for different property type specifications and requirements.
/// </remarks>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropertyAssetController(
    IMapper mapper,
    ILogger<PropertyAssetController> logger,
    IMessageProviderService messageProviderService,
    IPropertyAssetService propertyService,


    ICRUDExtender<PropertyAssetDto, CreatePropertyAssetRequest, UpdatePropertyAssetRequest> entityCRUDExtender,
    IValidator<CreatePropertyAssetRequest> entityCreateRequestValidator,
    IValidator<UpdatePropertyAssetRequest> entityUpdateRequestValidator
    )

    : CodedEntityOperationsControllerBase<PropertyAssetController, PropertyAssetDto, PropertyAssetResponse, CreatePropertyAssetRequest, UpdatePropertyAssetRequest>(
            logger, mapper, messageProviderService, propertyService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{


    [HttpGet("explicit")]
    [ProducesResponseType(typeof(PropertyAssetResponse), StatusCodes.Status200OK)]
    public new async Task<IActionResult> Get(ODataQueryOptions<PropertyAssetDto> queryOptions)
    {
        IQueryable<PropertyAssetDto>? queryable = null;
        var queryIncludes = _entityCRUDExtender.GetIncludes();
        if (queryIncludes != null)
            queryable = _crudService.GetAllAsQueryable(queryIncludes);
        else queryable = _crudService.GetAllAsQueryable();

        // Get the total count
        var totalRecords = queryable.Count();

        // Apply OData query options (including $top, $skip, etc.)
        var resultsQueryable = queryOptions.ApplyTo(queryable) as IQueryable<PropertyAssetDto>;

        IEnumerable<PropertyAssetDto> results = resultsQueryable == null ? [] : await resultsQueryable.ToListAsync();

        var explProps = results.Select(r => r.PropertyType == PropertyTypesEnum.Architectural ? (ArchitecturalPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Automotive ? (AutomotivePropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Commercial ? (CommercialPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Custom ? (CustomPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.EnergyEfficient ? (EnergyEfficientPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.GlassFilm ? (GlassFilmPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Outdoor ? (OutdoorPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Residential ? (ResidentialPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Signage ? (SignagePropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Specialty ? (SpecialtyPropertyAssetDto)r :
                                            r.PropertyType == PropertyTypesEnum.Other ? (OtherPropertyAssetDto)r :
                                            r).ToList();

        var explProps2 = results
                .Select<PropertyAssetDto, PropertyAssetDto?>(r => r.PropertyType switch
                {
                    PropertyTypesEnum.Architectural => r as ArchitecturalPropertyAssetDto,
                    PropertyTypesEnum.Automotive => r as AutomotivePropertyAssetDto,
                    PropertyTypesEnum.Commercial => r as CommercialPropertyAssetDto,
                    PropertyTypesEnum.Custom => r as CustomPropertyAssetDto,
                    PropertyTypesEnum.EnergyEfficient => r as EnergyEfficientPropertyAssetDto,
                    PropertyTypesEnum.GlassFilm => r as GlassFilmPropertyAssetDto,
                    PropertyTypesEnum.Outdoor => r as OutdoorPropertyAssetDto,
                    PropertyTypesEnum.Residential => r as ResidentialPropertyAssetDto,
                    PropertyTypesEnum.Signage => r as SignagePropertyAssetDto,
                    PropertyTypesEnum.Specialty => r as SpecialtyPropertyAssetDto,
                    PropertyTypesEnum.Other => r as OtherPropertyAssetDto,
                    _ => null // Handle unexpected cases
                })
                .Where(prop => prop != null)
                .ToList();

        IEnumerable<PropertyAssetResponse> resultsResponse = Mapper.Map<IEnumerable<PropertyAssetResponse>>(results);

        // Calculate paging info
        var pageSize = queryOptions.Top?.Value ?? totalRecords; // Default to totalRecords if $top is not provided
        var pageNo = queryOptions.Skip?.Value / pageSize + 1 ?? 1; // Default to page 1 if $skip is not provided
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        var pageRequest = new PageRequest() { PageNo = pageNo, PageSize = pageSize };

        var successResponse = new DefaultApiResponsePaged<IEnumerable<PropertyAssetResponse>>(
                                    resultsResponse, pageRequest, totalRecords, totalPages
                                );

        return CreateApiResponse(successResponse);


        //return await base.Get(queryOptions);
    }

    /*[HttpGet]
    [Route("properties")]
    public ActionResult<IEnumerable<object>> GetProperties()
    {
        return Ok(new object[] {
            new ArchitecturalPropertyDto { Id=Guid.NewGuid(), Code="P1", CustomerId=Guid.NewGuid(), Name = "ExampleArchitectural", PropertyType = PropertyTypesEnum.Architectural},
            new AutomotivePropertyDto { Id=Guid.NewGuid(), Code="P2", CustomerId=Guid.NewGuid(), Name = "ExampleAutomotive", PropertyType = PropertyTypesEnum.Automotive, Color="red", Make="Toyota", Model="Strada", Year=2001 }
        });
    }*/

    /*[HttpGet]
    [Route("properties")]
    public ActionResult<IEnumerable<PropertyDto>> GetProperties()
    {
        var properties = new PropertyDto[]
        {
            new ArchitecturalPropertyDto
            {
                Id = Guid.NewGuid(),
                Code = CodeGenerator.GenerateUniqueCode(Guid.NewGuid().ToString(), FieldLengths.General.CODE),
                CustomerCode = CodeGenerator.GenerateUniqueCode(Guid.NewGuid().ToString(), FieldLengths.Customer.Code),
                Name = "ExampleArchitectural",
                PropertyType = PropertyTypesEnum.Architectural
            },
            new AutomotivePropertyDto
            {
                Id = Guid.NewGuid(),
                Code = CodeGenerator.GenerateUniqueCode(Guid.NewGuid().ToString(), FieldLengths.General.CODE),
                CustomerCode = CodeGenerator.GenerateUniqueCode(Guid.NewGuid().ToString(), FieldLengths.Customer.Code),
                Name = "ExampleAutomotive",
                PropertyType = PropertyTypesEnum.Automotive,
                Color = "Red",
                Make = "Toyota",
                Model = "Strada",
                Year = 2001
            }
        };

        return Ok(properties);
    }*/

    /*[HttpPost]
    [Route("properties")]
    public IActionResult AddProperty([FromBody] PropertyDto property)
    {
        // Your logic here
        return Ok();
    }*/
}
