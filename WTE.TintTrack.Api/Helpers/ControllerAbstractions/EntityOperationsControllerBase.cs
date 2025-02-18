using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using SMEAppHouse.Core.Patterns.Repo.Paging;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Application.Shared.Messaging.Interface;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public abstract class EntityOperationsControllerBase<TController, TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>(
    ILogger<TController> logger,
    IMapper mapper,
    IMessageProviderService messageProviderService,
    ICRUDService<TEntityDto> crudService,
    IValidator<TEntityCreateRequest> entityCreateRequestValidator,
    ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest> entityCRUDExtender)
    : LoggingMappedControllerBase<TController>(logger, mapper, messageProviderService), IEntityOperationsController<TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>
    where TController : class
    where TEntityDto : GuidKeyedAuditableModel
    where TEntityResponse : class, IEntityResponse
    where TEntityCreateRequest : class, IEntityCreateRequest
    where TEntityUpdateRequest : class, IEntityUpdateRequest
{
    private readonly IValidator<TEntityCreateRequest> _entityCreateRequestValidator = entityCreateRequestValidator
        ?? throw new ArgumentNullException(nameof(entityCreateRequestValidator));

    protected readonly ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest> _entityCRUDExtender = entityCRUDExtender;

    /// <summary>
    /// CRUD service used for handling entity operations.
    /// </summary>
    protected readonly ICRUDService<TEntityDto> _crudService = crudService
        ?? throw new ArgumentNullException(nameof(crudService));

    /*/// <summary>
    /// Retrieves a [TEntityDto] identified by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing the requested entity if found, 
    /// or an appropriate failure response.
    /// </returns>
    /// <remarks>
    /// - Returns a 200 OK response if the entity is found.
    /// - Returns a 404 Not Found response if the entity does not exist.
    /// </remarks>
    /// <response code="200">The entity was successfully retrieved.</response>
    /// <response code="404">The entity with the specified identifier was not found.</response>
    [EnableQuery]
    [HttpGet("id={id}")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status404NotFound)]
    public IActionResult Get([FromODataUri] Guid id)
    {
        // Fetch the entity from the service
        var entityDto = _crudService.GetAllAsQueryable(p => p.Id == id).FirstOrDefault();

        if (entityDto == null)
        {
            var notFoundResponse = new ServiceFailureApiResponse<string>(string.Empty, StatusCodes.Status404NotFound,
                $"{typeof(TEntityDto).Name.Replace("Dto", string.Empty)} record not found.");
            return CreateApiResponse(notFoundResponse);
        }

        // Convert and return the entity wrapped in the response object
        var entityDtoResponse = Mapper.Map<TEntityResponse>(entityDto);
        var response = new DefaultApiResponse<TEntityResponse>(entityDtoResponse);

        return CreateApiResponse(response);
    }*/

    /// <summary>
    /// Retrieves a paginated list of [TEntityDto] objects based on the provided OData query options.
    /// </summary>
    /// <param name="queryOptions">The OData query options for filtering, sorting, and pagination.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a paginated list of entities that match the query options.
    /// </returns>
    /// <remarks>
    /// - Supports OData query options such as $filter, $orderby, $top, and $skip.
    /// - Automatically calculates pagination details including page number, page size, and total pages.
    /// </remarks>
    /// <response code="200">Returns a paginated list of entities.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(ODataQueryOptions<TEntityDto> queryOptions)
    {
        IQueryable<TEntityDto>? queryable = null;
        var queryIncludes = _entityCRUDExtender.GetIncludes();
        if (queryIncludes != null)
            queryable = _crudService.GetAllAsQueryable(queryIncludes);
        else queryable = _crudService.GetAllAsQueryable();

        // Get the total count
        var totalRecords = queryable.Count();

        // Apply OData query options (including $top, $skip, etc.)
        var resultsQueryable = queryOptions.ApplyTo(queryable) as IQueryable<TEntityDto>;
        //Console.WriteLine(resultsQueryable == null ? string.Empty : resultsQueryable.ToString());

        IEnumerable<TEntityDto> results = resultsQueryable == null ? [] : await resultsQueryable.ToListAsync();
        IEnumerable<TEntityResponse> resultsResponse = Mapper.Map<IEnumerable<TEntityResponse>>(results);

        // Calculate paging info
        var pageSize = queryOptions.Top?.Value ?? totalRecords; // Default to totalRecords if $top is not provided
        var pageNo = queryOptions.Skip?.Value / pageSize + 1 ?? 1; // Default to page 1 if $skip is not provided
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        var pageRequest = new PageRequest() { PageNo = pageNo, PageSize = pageSize };

        var successResponse = new DefaultApiResponsePaged<IEnumerable<TEntityResponse>>(
                                    resultsResponse, pageRequest, totalRecords, totalPages
                                );

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Creates a new [TEntityDto] based on the provided data.
    /// </summary>
    /// <param name="createEntityRequest">The request object containing the details of the entity to create.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the creation process.
    /// </returns>
    /// <remarks>
    /// - Validates the provided data using FluentValidation.
    /// - Checks for the existence of a similar record before creating a new entity.
    /// - Maps the creation request object to a DTO for database operations.
    /// </remarks>
    /// <response code="200">Indicates the entity was successfully created.</response>
    /// <response code="400">Indicates the request data is invalid or missing required fields.</response>
    /// <response code="409">Indicates a conflict occurred due to a duplicate record.</response>
    [HttpPost]
    [ProducesResponseType(typeof(IApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<dynamic>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<IEnumerable<string>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<dynamic>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TEntityCreateRequest createEntityRequest)
    {
        if (createEntityRequest == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR039");
            var failureResponse = new ServiceFailureApiResponse<TEntityCreateRequest>(
                                            createEntityRequest,
                                            apiMsg.Code, apiMsg.Message.Replace("{{TEntityDto}}", typeof(TEntityDto).Name.Replace("Dto", string.Empty)),
                                            statusCode: StatusCodes.Status400BadRequest
                                        );

            return CreateApiResponse(failureResponse);
        }

        var validationResult = await _entityCreateRequestValidator.ValidateAsync(createEntityRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<TEntityCreateRequest>(createEntityRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var entityDto = Mapper.Map<TEntityDto>(createEntityRequest);

        // Validate record existence
        if (await _entityCRUDExtender.ExistAsync(entityDto))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR040");
            var failureResponse = new ServiceFailureApiResponse<TEntityCreateRequest>(
                createEntityRequest,
                apiMsg.Code, apiMsg.Message.Replace("{{TEntityDto}}", typeof(TEntityDto).Name.Replace("Dto", string.Empty)),
                statusCode: StatusCodes.Status409Conflict
            );

            return CreateApiResponse(failureResponse);
        }

        var result = await _entityCRUDExtender.ExecuteAlternativeAsync(createEntityRequest);
        TEntityDto createdEntity;
        if (result.Success && result.createdEntity != null)
            createdEntity = await _crudService.AddAsync(result.createdEntity);
        else
            createdEntity = await _crudService.AddAsync(entityDto);

        var createdEntityResponse = Mapper.Map<TEntityResponse>(createdEntity);

        var infApiMsg = MessageProviderService.GetMessage("INF005");
        var successResponse = new DefaultApiResponse<TEntityResponse>(createdEntityResponse, StatusCodes.Status201Created, infApiMsg.Message);
        return CreateApiResponse(successResponse);
    }


    /// <summary>
    /// Deletes entities that match the specified OData query options.
    /// </summary>
    /// <remarks>
    /// Example usage: `/api/customer?$filter=Name eq 'SampleEntity'`
    /// </remarks>
    /// <param name="queryOptions">The OData query options used to filter the entities to delete.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> indicating the result of the delete operation.
    /// Returns a 400 Bad Request if query options are null.
    /// Returns a 404 Not Found if no entities match the query.
    /// Returns a 200 OK if the delete operation is successful.
    /// </returns>
    /// <response code="200">Entities were successfully deleted.</response>
    /// <response code="400">Query options are null or invalid.</response>
    /// <response code="404">No entities matched the specified query.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(DefaultApiResponse<dynamic>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(ODataQueryOptions<TEntityDto> queryOptions)
    {
        // example usage: /api/customer?$filter=Name eq 'SampleEntity'

        if (queryOptions == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR041");
            var errors = new List<ValidationFailure>
            {
                new()
                {
                    PropertyName = nameof(queryOptions),
                    ErrorMessage = apiMsg.Message
                }
            };
            return CreateApiResponse(new ValidationFailureApiResponse<string>(string.Empty, new ValidationResult(errors)));
        }

        var entitiesToDelete = queryOptions.ApplyTo(_crudService.GetAllAsQueryable()).Cast<TEntityDto>().ToList();
        if (entitiesToDelete.Count == 0)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR042");
            var failureResponse = new ServiceFailureApiResponse<string>(queryOptions.ToString() ?? string.Empty,
                apiMsg.Code, apiMsg.Message,
                statusCode: StatusCodes.Status404NotFound);
            return CreateApiResponse(failureResponse);
        }

        await _crudService.DeleteAsync(entitiesToDelete);
        var infApiMsg = MessageProviderService.GetMessage("INF010");
        return CreateApiResponse(new DefaultApiResponse<dynamic>(entitiesToDelete, StatusCodes.Status200OK, infApiMsg.Message ));
    }
}
