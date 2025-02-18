using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Request;

public class AddCustomerContactRequest
{
    [Required]
    [MaxLength(FieldLengths.Customer.Code)]
    public required string CustomerCode { get; set; }

    [Required]
    [MaxLength(FieldLengths.Contact.Code)]
    public required string ContactCode { get; set; }

    public required CustomerContactRelationshipTypesEnum RelationshipType { get; set; }
}
