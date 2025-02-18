using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;

namespace WTE.TintTrack.Business.Domain.Entities;

/// <summary>
/// Represents the ownership association between a user and a customer, 
/// supporting ownership roles for managing customer prospects.
/// </summary>
public class CustomerOwnership : GuidKeyedAuditableEntity
{
    public required string UserCode { get; set; }
    public bool? UserIsOwner { get; set; }

    // Navigation property representing the associated customer entity
    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
