using WTE.TintTrack.Api.Messaging._Abstractions;

namespace WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;

public class UpdatePropertyAssetRequest : ApiMessageRequest, IEntityUpdateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    //public PropertyTypesEnum? PropertyType { get; set; }
}