using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;

public class UserTenantInvitation : GuidKeyedAuditableEntity
{
    public required string EmailAddress { get; set; }
    //public required string FullName { get; set; }
    public required TenantInvitationStatusEnum InvitationStatus { get; set; }
    public required InvitationSourcesEnum InvitationSource { get; set; }

    // TODO: Add Expirable token to add to recipient email invitation link.

    public Guid? TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }

    public Guid? UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}