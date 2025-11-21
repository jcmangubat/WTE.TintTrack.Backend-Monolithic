using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Business.Requests.Customer;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Business;

public class CreateCustomerRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<CreateCustomerRequest>(identityOptions)
{
}
