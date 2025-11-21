using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.old;

public class TintServiceDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public string Name { get; set; } // Name of the service (e.g., "Ceramic Window Tinting")
    public string Description { get; set; } // Description of the service
    public decimal Price { get; set; } // Price for the service
    public TintServiceTypesEnum ServiceType { get; set; } // Type of service (e.g., "Automotive", "Residential", "Commercial")
    public string MaterialUsed { get; set; } // Description of the tinting film used (e.g., "Ceramic Film", "UV Protection Film")
    public bool IsAvailable { get; set; } // Availability of the service
    public int EstimatedDurationMinutes { get; set; } // Estimated time in minutes for completion
    public string AdditionalFeatures { get; set; } // Any additional features of the service (e.g., "Lifetime warranty", "UV blocking", "Heat rejection")

    public IEnumerable<InquiryDto> Inquiries { get; set; } = [];  // Associated inquiries for this service

    public IEnumerable<EstimateDto> Estimates { get; set; } = []; // Associated estimates for this service
    public IEnumerable<QuoteDto> Quotes { get; set; } = [];  // Associated quotes for this service
    public IEnumerable<ProposalDto> Proposals { get; set; } = [];  // Associated proposals for this service
}
