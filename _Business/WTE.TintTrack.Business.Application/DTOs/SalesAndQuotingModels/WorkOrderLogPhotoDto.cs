using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs.SalesAndQuotingModels;

public class WorkOrderLogPhotoDto : GuidKeyedAuditableModel
{
    public required string FileCode { get; set; }
    public required string FileUrl { get; set; }

    public required Guid WorkOrderLogId { get; set; }
    public virtual WorkOrderLogDto WorkOrderLog { get; set; }
}
