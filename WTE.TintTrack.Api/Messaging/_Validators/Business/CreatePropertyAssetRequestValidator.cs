using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WTE.TintTrack.Api.Messaging.Business.Requests.PropertyAsset;
using WTE.TintTrack.Application.Shared.Validator;

namespace WTE.TintTrack.Api.Messaging._Validators.Business;

public class CreatePropertyAssetRequestValidator(IOptions<IdentityOptions> identityOptions)
: AutoValidator<CreatePropertyAssetRequest>(identityOptions)
{
}
