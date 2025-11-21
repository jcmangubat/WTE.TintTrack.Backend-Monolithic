using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class SubscriptionPlanDiscountRequest
{
    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanDiscount.Code)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(FieldLengths.SubscriptionPlanDiscount.Name)]
    public required string Name { get; set; }

    [Required]
    public required decimal Percentage { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }

    [Required]
    public required DateTime EndDate { get; set; }
}