using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.TintServiceModels;

public class TintServiceDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public string Name { get; set; } // Name of the service (e.g., "Ceramic Window Tinting")
    public string Description { get; set; } // Description of the service
    public decimal Price { get; set; } // Price for the service
    public TintServiceTypesEnum ServiceType { get; set; } // Type of service (e.g., "Automotive", "Residential", "Commercial")
    public int EstimatedDurationMinutes { get; set; } // Estimated time in minutes for completion
    public string AdditionalFeatures { get; set; } // Any additional features of the service (e.g., "Lifetime warranty", "UV blocking", "Heat rejection")

    //public ICollection<EstimateItemDto> EstimateItems { get; set; } = new HashSet<EstimateItemDto>(); // Associated estimates for this service
    //public ICollection<QuoteItemDto> QuoteItems { get; set; } = new HashSet<QuoteItemDto>();  // Associated quotes for this service
    //public ICollection<ProposalItemDto> ProposalItems { get; set; } = new HashSet<ProposalItemDto>();  // Associated proposals for this service

    //public ICollection<WorkOrderItemDto> WorkOrderItems { get; set; } = new HashSet<WorkOrderItemDto>();

    public ICollection<TintServicePriceScheduleDto> TintServicePriceSchedules { get; set; } = new HashSet<TintServicePriceScheduleDto>();
}
