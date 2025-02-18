using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

public class InquiryDto : GuidKeyedAuditableModel
{
    /// <summary>
    /// Gets or sets the method by which the customer initiated the inquiry (e.g., Phone, Website, In-Person).
    /// </summary>
    public required LeadSourcesEnum LeadSource { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the consultation.
    /// </summary>
    public required DateTime ConsultationDate { get; set; }

    /// <summary>
    /// Gets or sets additional details about the consultation, such as customer preferences and budget.
    /// </summary>
    public required string Details { get; set; }

    public required string Subject{ get; set; }

    /// <summary>
    /// Gets or sets the type of tinting service the customer is interested in (e.g., Car, House, Office).
    /// </summary>
    public required PropertyTypesEnum PropertyType { get; set; }

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
    public string? SpecialRequests { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating if follow-up is required after the consultation.
    /// </summary>
    public bool? FollowUpNeeded { get; set; }

    /// <summary>
    /// Gets or sets the identifier for the proposal that may result from this consultation.
    /// </summary>
    public string? ProposalCode { get; set; }

    /// <summary>
    /// Gets or sets the user code of the sales representative handling this consultation.
    /// It can be null if a user has not handled this customer inquiry
    /// </summary>
    public string? SalesRepUserCode { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual CustomerDto? Customer { get; set; }
}
