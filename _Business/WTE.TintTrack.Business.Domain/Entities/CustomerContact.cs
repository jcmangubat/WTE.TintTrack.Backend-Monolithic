using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using WTE.TintTrack.Domain.Shared;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Domain.Entities;

// Join entity for the many-to-many relationship between Customer and Contact
public class CustomerContact : GuidKeyedAuditableEntity, ICodedEntity
{
    public required string Code { get; set; }

    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public required Guid ContactId { get; set; }
    public virtual Contact Contact { get; set; }

    public required CustomerContactRelationshipTypesEnum RelationshipType { get; set; } // Relationship type: Primary, Billing, etc.

}
