using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Responses;

public class CustomerContactResponse
{
    public required string CustomerCode { get; set; }

    public required string ContactCode { get; set; }
    
    public required CustomerContactRelationshipTypesEnum RelationshipType { get; set; } 
}
