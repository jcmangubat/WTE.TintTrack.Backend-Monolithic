using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class ContractMilestoneDto : GuidKeyedAuditableEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? ExpectedDate { get; set; }
    public decimal? Amount { get; set; }  // If billing per milestone

    public Guid ContractId { get; set; }
    public ContractDto Contract { get; set; } = null!;
}
