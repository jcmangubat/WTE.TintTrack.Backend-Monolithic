using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Project : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required decimal EstimatedCost { get; set; }
    public required decimal ActualCost { get; set; }
    public TaxExemptionReasonsEnum? TaxExemptionReason { get; set; } = TaxExemptionReasonsEnum.NotExempt;

    // Foreign Key: A Project is linked to a specific Quote
    public required Guid QuoteId { get; set; } // Foreign Key to Quote
    public virtual Quote Quote { get; set; }

    public virtual ICollection<ProjectTask> ProjectActivities { get; set; } = new HashSet<ProjectTask>();
}
