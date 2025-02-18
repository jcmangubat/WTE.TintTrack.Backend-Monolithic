using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Application.Shared.Validator;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

namespace WTE.TintTrack.Core.Application.Validators;

public class SubscriptionPlanDiscountDtoValidator(IOptions<IdentityOptions> identityOptions)
    : AutoValidator<SubscriptionPlanDiscountDto>(identityOptions)
{
}