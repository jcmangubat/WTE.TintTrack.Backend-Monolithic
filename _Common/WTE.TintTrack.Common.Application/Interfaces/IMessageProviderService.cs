using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface IMessageProviderService
{
    APIMessage GetMessage(string code, string? locale = null, Dictionary<string, string>? templateVals = null);
    ServiceOperationException ServiceOperationException(string errorCode, string locale = "en");
    CustomSecurityTokenException CustomSecurityTokenException(string errorCode, string locale = "en");
}
