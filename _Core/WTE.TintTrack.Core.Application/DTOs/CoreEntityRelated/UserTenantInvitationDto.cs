using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
public class UserTenantInvitationDto : GuidKeyedAuditableModel
{
    [Required]
    [Email]
    public required string EmailAddress { get; set; }
    
    /*[Required]
    public required string FullName { get; set; }*/

    [Required]
    public required TenantInvitationStatusEnum InvitationStatus { get; set; }

    [Required]
    public required InvitationSourcesEnum InvitationSource { get; set; }

    // TODO: Add Expirable token to add to recipient email invitation link.

    public virtual TenantDto Tenant { get; set; }

    public virtual ApplicationUserDto User { get; set; }
}