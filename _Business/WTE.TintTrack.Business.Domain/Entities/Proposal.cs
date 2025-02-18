using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Business.Domain.Entities;

public class Proposal : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required string ProposalNumber { get; set; }
    public required string Terms { get; set; }
    public required string ScopeOfWork { get; set; }
    public required decimal TotalAmount { get; set; }

    public Guid? InquiryId { get; set; }
    public virtual Inquiry? Inquiry { get; set; }

    public required string CustomerContactCode { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();

    public virtual ICollection<ProposalMember> ProposalMembers { get; set; } = new HashSet<ProposalMember>();
}
