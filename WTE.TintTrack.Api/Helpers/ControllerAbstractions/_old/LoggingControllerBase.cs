using Microsoft.AspNetCore.Mvc;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public class LoggingControllerBase<TController>(ILogger<TController> logger) : ControllerBase
{
    protected readonly ILogger<TController> Logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
}
