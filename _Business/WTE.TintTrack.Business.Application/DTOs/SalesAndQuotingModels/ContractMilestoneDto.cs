using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class ContractMilestoneDto : GuidKeyedAuditableModel, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public decimal? EstimatedAmount { get; set; } // Optional: money tied to milestone if billing phased

    public Guid ContractId { get; set; }
    public ContractDto Contract { get; set; } = null!;
}
