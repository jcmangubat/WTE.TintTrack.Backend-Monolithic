using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions.Interfaces;

public interface IEntityOperationsController<TEntityDto, TEntityResponse, TEntityCreateRequest, TEntityUpdateRequest>
    where TEntityDto : class
    where TEntityResponse : class, IEntityResponse
    where TEntityCreateRequest : class, IEntityCreateRequest
    where TEntityUpdateRequest : class, IEntityUpdateRequest
{
    //IActionResult Get([FromODataUri] Guid id);
    Task<IActionResult> Get(ODataQueryOptions<TEntityDto> queryOptions);
    Task<IActionResult> Create(TEntityCreateRequest customerDto);
    Task<IActionResult> Delete(ODataQueryOptions<TEntityDto> queryOptions);
}
