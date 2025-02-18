using WTE.TintTrack.Api.Messaging._Abstractions;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Api.Messaging.Business.Request;

public class UpdateInquiryRequest : ApiMessageRequest, IEntityUpdateRequest
{

    /// <summary>
    /// Gets or sets the method by which the customer initiated the inquiry (e.g., Phone, Website, In-Person).
    /// </summary>
    public LeadSourcesEnum? LeadSource { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the consultation.
    /// </summary>
    public DateTime? ConsultationDate { get; set; }

    /// <summary>
    /// Gets or sets additional details about the consultation, such as customer preferences and budget.
    /// </summary>
    [MaxLength(FieldLengths.Inquiry.Details)]
    public string? Details { get; set; }

    [MaxLength(FieldLengths.Inquiry.Subject)]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the type of tinting service the customer is interested in (e.g., Car, House, Office).
    /// </summary>
    public PropertyTypesEnum? PropertyType { get; set; }

    /// <summary>
    /// Gets or sets the estimated budget range or specifics mentioned by the customer during consultation.
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Gets or sets the specific tint type requested by the customer (e.g., Standard, Ceramic, Reflective).
    /// </summary>
    public TintTypesEnum? TintType { get; set; }

    /// <summary>
    /// Gets or sets any special requests or custom preferences the customer may have regarding the service.
    /// </summary>
    [MaxLength(FieldLengths.Inquiry.SpecialRequests)]
    public string? SpecialRequests { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if follow-up is required after the consultation.
    /// </summary>
    public bool? FollowUpNeeded { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the proposal that may result from this consultation.
    /// </summary>
    [MaxLength(FieldLengths.Inquiry.ProposalCode)]
    public string? ProposalCode { get; set; }

    /// <summary>
    /// Gets or sets the user code of the sales representative handling this consultation.
    /// It can be null if a user has not handled this customer inquiry
    /// </summary>
    [MaxLength(FieldLengths.Inquiry.SalesRepUserCode)]
    public string? SalesRepUserCode { get; set; }

    // Navigation property representing the associated customer entity
    [MaxLength(FieldLengths.Customer.Code)]
    public string? CustomerCode { get; set; }
}
