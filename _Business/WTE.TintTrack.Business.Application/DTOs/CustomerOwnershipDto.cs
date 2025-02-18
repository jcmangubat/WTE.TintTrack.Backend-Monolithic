using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Business.Domain.Entities;

namespace WTE.TintTrack.Business.Application.DTOs;

/// <summary>
/// Represents the ownership association between a user and a customer, 
/// supporting ownership roles for managing customer prospects.
/// </summary>
public class CustomerOwnershipDto : GuidKeyedAuditableModel
{
    public required string UserCode { get; set; }
    public bool? UserIsOwner { get; set; }

    // Navigation property representing the associated customer entity
    public required Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
