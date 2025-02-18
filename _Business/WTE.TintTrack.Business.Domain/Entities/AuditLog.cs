using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities;

public class AuditLog : GuidKeyedAuditableEntity
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
