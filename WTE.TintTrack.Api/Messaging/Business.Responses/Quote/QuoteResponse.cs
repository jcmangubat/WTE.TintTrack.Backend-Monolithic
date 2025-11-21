using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Business.Responses.Quote;

public class QuoteResponse : ApiMessageResponse, IEntityResponse
{
    [Required]
    [MaxLength(FieldLengths.General.CODE)]
    public required string Code { get; set; }
}
