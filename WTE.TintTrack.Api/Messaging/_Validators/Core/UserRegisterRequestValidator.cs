using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Core;

public class UserRegisterRequestValidator : AutoValidator<UserRegisterRequest>
{
    public UserRegisterRequestValidator(IOptions<IdentityOptions> identityOptions) : base(identityOptions)
    {
        /* // Rule 1: Validate RoleDesignation when TenantCode is supplied
         RuleFor(x => x.Role)
             .NotEmpty()
             .Must(value => Enum.TryParse(typeof(UserRolesEnum), value, out _))
             .WithMessage("RoleDesignation must be a valid UserRolesEnum value when TenantCode is supplied.")
             .When(x => !string.IsNullOrEmpty(x.TenantCode));

         // Rule 2: Only validate RoleDesignation if TenantName is provided without TenantCode
         RuleFor(x => x.Role)
             .Equal(UserRolesEnum.TenantOwner.ToString())
             .WithMessage("RoleDesignation must be TenantOwner when TenantName is provided and TenantCode is empty.")
             .When(x => !string.IsNullOrEmpty(x.TenantName) && string.IsNullOrEmpty(x.TenantCode));*/
    }
}
