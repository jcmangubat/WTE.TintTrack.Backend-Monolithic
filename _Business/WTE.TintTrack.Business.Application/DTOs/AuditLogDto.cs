using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs;

public class AuditLogDto : GuidKeyedAuditableModel
{
    /// <summary>
    /// The user who performed the action
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// Date and time the action was performed
    /// </summary>
    public required DateTime ActionDate { get; set; }

    /// <summary>
    /// Description of the action performed
    /// </summary>
    public required string Action { get; set; }
}
