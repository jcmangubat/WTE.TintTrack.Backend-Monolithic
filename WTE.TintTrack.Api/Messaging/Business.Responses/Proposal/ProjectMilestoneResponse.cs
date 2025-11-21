using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Responses.Proposal;

public class ProjectMilestoneResponse : ApiMessageResponse, IEntityResponse
{
    [Required]
    [MaxLength(FieldLengths.General.CODE)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(FieldLengths.General.SummaryParagraph)]
    public required string Terms { get; set; }

    [Required]
    [MaxLength(FieldLengths.General.SummaryParagraph)]
    public required string ScopeOfWork { get; set; }

    [Required]
    public required decimal TotalAmount { get; set; }

    [MaxLength(FieldLengths.General.CODE)]
    public required string InquiryCode { get; set; }

    [Required]
    [MaxLength(FieldLengths.General.CODE)]
    public required string CustomerContactCode { get; set; }

    [Required]
    public OfferDocumentStatusEnum Status { get; set; } = OfferDocumentStatusEnum.Draft;

    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
}
