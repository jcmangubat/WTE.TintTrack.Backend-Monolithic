using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities;

public class AuditLog : GuidKeyedAuditableEntity
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
