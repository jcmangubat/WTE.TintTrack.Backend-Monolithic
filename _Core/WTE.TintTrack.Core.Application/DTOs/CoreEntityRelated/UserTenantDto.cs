using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class UserTenantDto : GuidKeyedAuditableModel
{
    public bool? IsDefault { get; set; }
    public bool? UserIsOwner { get; set; }

    [Required]
    public required Guid UserId { get; set; }
    public virtual ApplicationUserDto User { get; set; }

    [Required]
    public required Guid TenantId { get; set; }
    public virtual TenantDto Tenant { get; set; }

    public IList<UserTenantRoleDto> UserTenantRoles { get; set; } = [];
}