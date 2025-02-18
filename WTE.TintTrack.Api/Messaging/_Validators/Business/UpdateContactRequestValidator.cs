using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Business.Request;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Business;

public class UpdateContactRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<UpdateContactRequest>(identityOptions)
{
}