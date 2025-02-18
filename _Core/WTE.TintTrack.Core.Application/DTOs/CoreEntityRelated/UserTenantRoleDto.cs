using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class UserTenantRoleDto : GuidKeyedAuditableModel
{
    [Required]
    public required Guid UserTenantId { get; set; }
    public virtual UserTenantDto UserTenant { get; set; }

    [Required]
    public required Guid RoleId { get; set; }
    public virtual ApplicationRoleDto Role { get; set; }
}
