using Microsoft.AspNetCore.Mvc;
using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;

public interface ICodedEntityOperationsController<TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>
    : IEntityOperationsController<TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>
    where TEntityDto : GuidKeyedAuditableModel
    where TEntityResponse : class, IEntityResponse
    where TEntityCreateRequest : class, IEntityCreateRequest
    where TEntityUpdateRequest : class, IEntityUpdateRequest
{
    Task<IActionResult> GetByCode(string code);
    Task<IActionResult> Update(string code, TEntityUpdateRequest updateEntityRequest);
}


/*public async Task<IActionResult> Update(string code, TEntityDto entityDto)
{
    try
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Code is required.");

        if (entityDto == null)
            return BadRequest("Entity cannot be null.");

        var existingEntity = await _crudService.FindByAsync(e => e.Id == code);
        if (!existingEntity.Any())
            return NotFound($"Entity with code '{code}' not found.");

        var updatedEntity = await _crudService.UpdateAsync(entityDto);
        return Ok(updatedEntity);
    }
    catch (ValidationException ex)
    {
        Logger.LogError(ex, ex.GetExceptionMessages());
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        Logger.LogError(ex, ex.GetExceptionMessages());
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the entity.");
    }
}*/