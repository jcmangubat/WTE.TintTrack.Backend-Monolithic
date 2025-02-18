using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.Repo.Paging;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Api.Messaging.Business.Responses;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Helpers;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Controllers.Business;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PropertyController(
    IMapper mapper,
    ILogger<PropertyController> logger,
    IMessageProviderService messageProviderService,
    IPropertyService propertyService,


    ICRUDExtender<PropertyDto, CreatePropertyRequest, UpdatePropertyRequest> entityCRUDExtender,
    IValidator<CreatePropertyRequest> entityCreateRequestValidator,
    IValidator<UpdatePropertyRequest> entityUpdateRequestValidator
    )

    : CodedEntityOperationsControllerBase<PropertyController, PropertyDto, PropertyResponse, CreatePropertyRequest, UpdatePropertyRequest>(
            logger, mapper, messageProviderService, propertyService, entityCRUDExtender, entityCreateRequestValidator, entityUpdateRequestValidator)
{


    [HttpGet("explicit")]
    [ProducesResponseType(typeof(PropertyResponse), StatusCodes.Status200OK)]
    public new async Task<IActionResult> Get(ODataQueryOptions<PropertyDto> queryOptions)
    {
        IQueryable<PropertyDto>? queryable = null;
        var queryIncludes = _entityCRUDExtender.GetIncludes();
        if (queryIncludes != null)
            queryable = _crudService.GetAllAsQueryable(queryIncludes);
        else queryable = _crudService.GetAllAsQueryable();

        // Get the total count
        var totalRecords = queryable.Count();

        // Apply OData query options (including $top, $skip, etc.)
        var resultsQueryable = queryOptions.ApplyTo(queryable) as IQueryable<PropertyDto>;

        IEnumerable<PropertyDto> results = resultsQueryable == null ? [] : await resultsQueryable.ToListAsync();

        var explProps = results.Select(r => r.PropertyType == PropertyTypesEnum.Architectural ? (ArchitecturalPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Automotive ? (AutomotivePropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Commercial ? (CommercialPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Custom ? (CustomPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.EnergyEfficient ? (EnergyEfficientPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.GlassFilm ? (GlassFilmPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Outdoor ? (OutdoorPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Residential ? (ResidentialPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Signage ? (SignagePropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Specialty ? (SpecialtyPropertyDto)r :
                                            r.PropertyType == PropertyTypesEnum.Other ? (OtherPropertyDto)r :
                                            r).ToList();

        var explProps2 = results
                .Select<PropertyDto, PropertyDto?>(r => r.PropertyType switch
                {
                    PropertyTypesEnum.Architectural => r as ArchitecturalPropertyDto,
                    PropertyTypesEnum.Automotive => r as AutomotivePropertyDto,
                    PropertyTypesEnum.Commercial => r as CommercialPropertyDto,
                    PropertyTypesEnum.Custom => r as CustomPropertyDto,
                    PropertyTypesEnum.EnergyEfficient => r as EnergyEfficientPropertyDto,
                    PropertyTypesEnum.GlassFilm => r as GlassFilmPropertyDto,
                    PropertyTypesEnum.Outdoor => r as OutdoorPropertyDto,
                    PropertyTypesEnum.Residential => r as ResidentialPropertyDto,
                    PropertyTypesEnum.Signage => r as SignagePropertyDto,
                    PropertyTypesEnum.Specialty => r as SpecialtyPropertyDto,
                    PropertyTypesEnum.Other => r as OtherPropertyDto,
                    _ => null // Handle unexpected cases
                })
                .Where(prop => prop != null)
                .ToList();

        IEnumerable<PropertyResponse> resultsResponse = Mapper.Map<IEnumerable<PropertyResponse>>(results);

        // Calculate paging info
        var pageSize = queryOptions.Top?.Value ?? totalRecords; // Default to totalRecords if $top is not provided
        var pageNo = queryOptions.Skip?.Value / pageSize + 1 ?? 1; // Default to page 1 if $skip is not provided
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        var pageRequest = new PageRequest() { PageNo = pageNo, PageSize = pageSize };

        var successResponse = new DefaultApiResponsePaged<IEnumerable<PropertyResponse>>(
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
