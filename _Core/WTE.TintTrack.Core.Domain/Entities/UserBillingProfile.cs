using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Domain.Entities;

/// <summary>
/// A user may have several billing profiles
/// </summary>
public class UserBillingProfile : GuidKeyedAuditableEntity
{
    public required string BillingAddress { get; set; }

    public required BillingProfileTypesEnum BillingProfileType { get; set; }

    /// <summary>
    /// JSON string to store serialized billing details.
    /// </summary>
    public required string BillingDetailsJson { get; set; }


    public required Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}