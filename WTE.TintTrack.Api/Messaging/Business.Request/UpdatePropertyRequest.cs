using WTE.TintTrack.Api.Messaging._Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Request;

public class UpdatePropertyRequest : ApiMessageRequest, IEntityUpdateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    //public PropertyTypesEnum? PropertyType { get; set; }
}