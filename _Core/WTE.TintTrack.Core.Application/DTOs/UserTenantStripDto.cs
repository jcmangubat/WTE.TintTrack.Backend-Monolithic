using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Application.DTOs;

public class UserTenantStripDto
{
    public bool? IsDefault { get; set; }
    public bool? UserIsOwner { get; set; }

    public string TenantCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Consts.TenantStatusEnum Status { get; set; }
    public string StatusText { get; set; }
}
