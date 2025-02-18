using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Application.Shared.Validator.Attributes;
using static WTE.TintTrack.Common.Constants.Consts;
using WTE.TintTrack.Domain.Shared.BillingProfile.Abstractions;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class UserBillingProfileDto : GuidKeyedAuditableModel
{
    [Required]
    [MaxLength(FieldLengths.UserBillingProfile.BillingAddress)]
    public required string BillingAddress { get; set; }

    [Required]
    public required BillingProfileTypesEnum BillingProfileType { get; set; }

    [Required]
    [MaxLength(FieldLengths.UserBillingProfile.BillingDetailsJson)]
    public required string BillingDetailsJson { get; set; }

    public IBillingDetails BillingDetails { get; set; }

    [Required]
    public required string UserCode { get; set; }
}
