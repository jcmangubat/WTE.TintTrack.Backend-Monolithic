using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Business.Requests.Contact;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Business;

public class CreateContactRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<CreateContactRequest>(identityOptions)
{
}
