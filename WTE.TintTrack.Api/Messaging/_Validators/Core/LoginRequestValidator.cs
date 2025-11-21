using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Core.Requests;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Core;

public class LoginRequestValidator(IOptions<IdentityOptions> identityOptions)
    : AutoValidator<LoginRequest>(identityOptions)
{

}
