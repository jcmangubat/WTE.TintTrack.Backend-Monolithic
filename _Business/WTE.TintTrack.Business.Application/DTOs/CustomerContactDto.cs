using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

// Join entity for the many-to-many relationship between Customer and Contact
public class CustomerContactDto : GuidKeyedAuditableModel
{
    public required Guid CustomerId { get; set; }
    public virtual CustomerDto Customer { get; set; }

    public required Guid ContactId { get; set; }
    public virtual ContactDto Contact { get; set; }

    public required CustomerContactRelationshipTypesEnum RelationshipType { get; set; } // Relationship type: Primary, Billing, etc.
}