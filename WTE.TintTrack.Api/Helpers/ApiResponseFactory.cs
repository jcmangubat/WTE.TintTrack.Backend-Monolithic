using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Application.Shared.Messaging.Interface;

namespace WTE.TintTrack.Api.Helpers;

public static class ApiResponseFactory
{
    public static IActionResult CreateApiResponse(IApiResponse response) =>
        new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
}