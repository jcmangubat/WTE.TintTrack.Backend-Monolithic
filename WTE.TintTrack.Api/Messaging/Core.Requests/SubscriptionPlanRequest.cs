using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class SubscriptionPlanRequest
{
    [Required]
    [MaxLength(FieldLengths.SubscriptionPlan.Name)]
    public required string Name { get; set; }

    [MaxLength(FieldLengths.SubscriptionPlan.PlanCode)]
    public required string PlanCode { get; set; }

    [Required]
    public decimal Price { get; set; }

    [NumericRange(1, 10000)]
    public int? MaxUsers { get; set; }


    public bool? IsActive { get; set; } = true;


    public bool? IsArchived { get; set; }

    public DateTime? DateArchived { get; set; }

    public string? ReasonArchived { get; set; }

}
