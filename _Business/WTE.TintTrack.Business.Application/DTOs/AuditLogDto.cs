using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;

namespace WTE.TintTrack.Business.Application.DTOs;

public class AuditLogDto : GuidKeyedAuditableModel
{
    /// <summary>
    /// Date and time the action was executed
    /// </summary>
    public required DateTime ActionDate { get; set; }

    /// <summary>
    /// The user who executed an action
    /// </summary>
    public  string? UserCode { get; set; }

    /// <summary>
    /// The entity name on which the action was executed
    /// </summary>
    public string EntityName { get; set; } = default!;

    /// <summary>
    /// Description of the action executed
    /// </summary>
    public required string ActionData { get; set; }
}
