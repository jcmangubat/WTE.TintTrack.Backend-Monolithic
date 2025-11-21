using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Domain.Entities.SalesAndQuotingEntities;

public class ContractMilestone : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public DateTime? ExpectedStartDate { get; set; }
    public DateTime? ExpectedEndDate { get; set; }
    public decimal? EstimatedAmount { get; set; } // Optional: money tied to milestone if billing phased

    public Guid ContractId { get; set; }
    public Contract Contract { get; set; } = null!;
}
