using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Core.Request;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Core;

public class UserTenantRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<UserTenantRequest>(identityOptions)
{
}