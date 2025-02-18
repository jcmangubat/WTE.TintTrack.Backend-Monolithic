using Microsoft.AspNetCore.Identity;

namespace WTE.TintTrack.Core.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
}
