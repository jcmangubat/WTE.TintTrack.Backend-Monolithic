using WTE.TintTrack.Application.Shared.Validator.Attributes;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Api.Messaging.Core.Requests;

public class UserBillingProfileRequest
{
    [Required]
    [MaxLength(FieldLengths.UserBillingProfile.BillingAddress)]
    public string BillingAddress { get; set; }

    [Required]
    public Consts.BillingProfileTypesEnum BillingProfileType { get; set; }

    [Required]
    public string BillingDetailsJson { get; set; }

    [Required]
    public string UserCode { get; set; }
}