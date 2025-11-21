using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Application.DTOs.old.SalesAndQuoting;

public class WorkLogPhotoDto : GuidKeyedAuditableEntity
{
    public required string FileCode { get; set; }
    public required string FileUrl { get; set; }

    public required Guid WorkLogId { get; set; }
    public virtual WorkLogDto WorkLog { get; set; }
}
