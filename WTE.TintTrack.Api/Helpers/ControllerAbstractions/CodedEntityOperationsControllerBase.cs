using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;
using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.Messaging;
using WTE.TintTrack.Application.Shared.Messaging.Interface;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public abstract class CodedEntityOperationsControllerBase<TController, TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>(
        ILogger<TController> logger,
        IMapper mapper,
        IMessageProviderService messageProviderService,
        ICRUDService<TEntityDto> crudService,
        ICRUDExtender<TEntityDto, TEntityCreateRequest, TEntityUpdateRequest> entityCRUDExtender,
        IValidator<TEntityCreateRequest> entityCreateRequestValidator,
        IValidator<TEntityUpdateRequest> entityUpdateRequestValidator
    )

    : EntityOperationsControllerBase<TController, TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>(
            logger, mapper, messageProviderService, crudService, entityCreateRequestValidator, entityCRUDExtender
        ), ICodedEntityOperationsController<TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>

    where TController : class
    where TEntityDto : GuidKeyedAuditableModel, ICodedEntity
    where TEntityResponse : class, IEntityResponse
    where TEntityCreateRequest : class, IEntityCreateRequest
    where TEntityUpdateRequest : class, IEntityUpdateRequest
{
    private readonly IValidator<TEntityUpdateRequest> _entityUpdateRequestValidator = entityUpdateRequestValidator;

    /// <summary>
    /// Retrieves an [TEntityDto] by its code.
    /// </summary>
    /// <param name="code">The unique code of the [TEntityDto] to retrieve.</param>
    /// <returns>
    /// An <see cref="IApiResponse"/> containing the [TEntityDto] details if found, 
    /// or an appropriate failure response.
    /// </returns>
    /// <remarks>
    /// - Returns a 200 OK response if the [TEntityDto] is found.
    /// - Returns a 400 Bad Request response if the code is invalid or the entity is not found.
    /// </remarks>
    /// <response code="200">The [TEntityDto] was successfully retrieved.</response>
    /// <response code="400">
    /// - Validation failure if the code is null or empty.
    /// - Service failure if the entity with the specified code does not exist.
    /// </response>
    [HttpGet("{code}")]
    [ProducesResponseType(typeof(IApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetByCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR043");
            var errors = new List<ValidationFailure>
            {
                new()
                {
                    PropertyName = nameof(code),
                    ErrorMessage = apiMsg.Message
                }
            };
            return CreateApiResponse(new ValidationFailureApiResponse<string>(string.Empty, new ValidationResult(errors)));
        }

        var includes = _entityCRUDExtender.GetIncludes();
        var entityDto = await _crudService.FindSingleAsync(e => e.Code == code, includes);

        /*IQueryable<TEntityDto>? queryable = null;
        var queryIncludes = _entityCRUDExtender.GetIncludes();
        if (queryIncludes != null)
            queryable = _crudService.GetAllAsQueryable(queryIncludes);
        else queryable = _crudService.GetAllAsQueryable();
        entityDto = await queryable.FirstOrDefaultAsync(e => e.Code == code);*/

        if (entityDto == null)
        {
            var typeName = typeof(TEntityDto).Name.Replace("Dto", string.Empty);
            var apiMsg = MessageProviderService.GetMessage("ERR044");
            var failureResponse = new ServiceFailureApiResponse<string>(code,
                                            apiMsg.Code, apiMsg.Message.Replace("{{typeName}}", typeName).Replace("{{code}}", code),
                                            statusCode: StatusCodes.Status400BadRequest);
            return CreateApiResponse(failureResponse);
        }

        var entityDtoResponse = Mapper.Map<TEntityResponse>(entityDto);
        var successResponse = new DefaultApiResponse<TEntityResponse>(entityDtoResponse);

        return CreateApiResponse(successResponse);
    }

    /// <summary>
    /// Updates an [TEntityDto] identified by its code with the provided update request data.
    /// </summary>
    /// <param name="code">The unique code of the [TEntityDto] to update.</param>
    /// <param name="updateEntityRequest">The data to update the [TEntityDto] with.</param>
    /// <returns>
    /// An <see cref="IApiResponse"/> containing the updated [TEntityDto] if successful, 
    /// or an appropriate failure response.
    /// </returns>
    /// <remarks>
    /// - Returns a 200 OK response if the [TEntityDto] is successfully updated.
    /// - Returns a 400 Bad Request response if the code or update data is invalid.
    /// - Returns a 404 Not Found response if the [TEntityDto] does not exist.
    /// </remarks>
    /// <response code="200">The [TEntityDto] was successfully updated.</response>
    /// <response code="400">
    /// - Validation failure if the code or updateEntityRequest is null or invalid.
    /// </response>
    /// <response code="404">The [TEntityDto] with the specified code was not found.</response>
    [HttpPut("{code}")]
    [ProducesResponseType(typeof(IApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationFailureApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string code, TEntityUpdateRequest updateEntityRequest)
    {
        if (string.IsNullOrEmpty(code))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR043");
            var errors = new List<ValidationFailure>
            {
                new()
                {
                    PropertyName = nameof(code),
                    ErrorMessage = apiMsg.Message
                }
            };
            return CreateApiResponse(new ValidationFailureApiResponse<string>(string.Empty, new ValidationResult(errors)));
        }

        if (updateEntityRequest == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR045");
            var errors = new List<ValidationFailure>
                {
                    new()
                    {
                        PropertyName = nameof(updateEntityRequest),
                        ErrorMessage = apiMsg.Message
                    }
                };
            return CreateApiResponse(new ValidationFailureApiResponse<string>(code, new ValidationResult(errors)));
        }

        var validationResult = await _entityUpdateRequestValidator.ValidateAsync(updateEntityRequest);
        if (!validationResult.IsValid)
        {
            var validationResponse = new ValidationFailureApiResponse<TEntityUpdateRequest>(updateEntityRequest, validationResult);
            return CreateApiResponse(validationResponse);
        }

        var entityDto = await _crudService.FindSingleAsync(p => p.Code == code);
        if (entityDto == null)
        {
            var apiMsg = MessageProviderService.GetMessage("ERR046");
            return CreateApiResponse(new ServiceFailureApiResponse<string>(code,
                    apiMsg.Code, apiMsg.Message
                                        .Replace("{{TEntityDto}}", nameof(TEntityDto).Replace("Dto", string.Empty))
                                        .Replace("{{code}}", code),
                    statusCode: StatusCodes.Status404NotFound));
        }

        entityDto = _entityCRUDExtender.TransformForUpdate(entityDto, updateEntityRequest);

        var updatedEntity = await _crudService.UpdateAsync(entityDto);
        var infApiMsg = MessageProviderService.GetMessage("INF011");
        var successResponse = new DefaultApiResponse<TEntityDto>(updatedEntity, infApiMsg.Message);

        return CreateApiResponse(successResponse);
    }
}