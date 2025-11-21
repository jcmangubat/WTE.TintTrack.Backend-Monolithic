using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

/// <summary>
/// Represents the initial consultation or inquiry from a customer regarding tint services.
/// Tracks details such as the customer’s needs, preferred service type, and consultation outcome.
/// </summary>
public class InquiryDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }

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
    public required string Subject { get; set; }

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
    /// Gets or sets the user code of the sales representative handling this consultation.
    /// It can be null if a user has not handled this customer inquiry
    /// </summary>
    public string? SalesRepUserCode { get; set; }

    public IEnumerable<string>? TintServiceCodes { get; set; }

    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContactDto CustomerContact { get; set; }

    //public ICollection<Proposal> Proposals { get; set; } = new HashSet<Proposal>();

    //public ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();

    //public ICollection<Estimate> Estimates { get; set; } = new HashSet<Estimate>();
}

