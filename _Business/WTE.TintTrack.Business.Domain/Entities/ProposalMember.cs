using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

public class ProposalMember : GuidKeyedAuditableEntity
{
    public required Guid CustomerContactId { get; set; }
    public virtual CustomerContact CustomerContact { get; set; }

    public ProposalMemberRolesEnum ProposalMemberRole { get; set; }

    public bool? Reviewed { get; set; }
    public bool? Approved { get; set; }

    public DateTime? DateReviewed { get; set; }
    public DateTime? DateApproved { get; set; }


    public required Guid ProposalId { get; set; }
    public virtual Proposal Proposal { get; set; }
}
