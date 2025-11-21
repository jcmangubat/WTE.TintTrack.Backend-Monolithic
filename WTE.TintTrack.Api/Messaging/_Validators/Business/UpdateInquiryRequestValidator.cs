using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Business.Requests.Inquiry;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Business;

public class UpdateInquiryRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<UpdateInquiryRequest>(identityOptions)
{
}
