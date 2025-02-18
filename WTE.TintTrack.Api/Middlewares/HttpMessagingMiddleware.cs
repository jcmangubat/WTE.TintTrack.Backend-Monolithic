using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Dynamic;
using System.Net;
using WTE.TintTrack.Application.Shared.Helpers;
using WTE.TintTrack.Common.Exceptions;

namespace WTE.TintTrack.Api.Middlewares;

public class HttpMessagingMiddleware(RequestDelegate next, ILogger<HttpMessagingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<HttpMessagingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            // Call the next middleware in the pipeline
            await _next(httpContext);

            // Check if the user is unauthorized
            if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                // Handle the authorization failure
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync((string)JsonConvert.SerializeObject(new
                {
                    httpContext.Response.StatusCode,
                    Message = "You are not authorized to access this resource.",
                    Success = false,
                    Errors = new { },
                    Data = new { }
                }));

                return;
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context.Response.HasStarted)
            return Task.CompletedTask;

        context.Response.ContentType = "application/json";

        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented // Makes JSON pretty-printed
        };

        dynamic errorDetails;

        // Handle different types of exceptions
        switch (exception)
        {
            case CustomValidationException customValidationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails = new
                {
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    customValidationEx.Errors,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            case RecordNotFoundException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails = new
                {
                    notFoundEx.ErrorCode,
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            case CustomInvalidOperationException invalidOpEx:
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                errorDetails = new
                {
                    invalidOpEx.ErrorCode,
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));
            case ServiceOperationException userSignInEx:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var errData = new { };

                var outputDict = (IDictionary<string, object>)new ExpandoObject();
                foreach (var item in userSignInEx.Errors)
                {
                    outputDict[item.Key] = string.Join(", ", item.Value);
                }

                errorDetails = new
                {
                    userSignInEx.ErrorCode,
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    Data = outputDict
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            case SecurityTokenException securityTokenEx:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorDetails = new
                {
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            case UnauthorizedAccessException or CustomUnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorDetails = new
                {
                    Success = false,
                    exception.Message,
                    context.Response.StatusCode,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails = new
                {
                    Success = false,
                    Message = "Validation errors occurred.",
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = new { },
                    Errors = validationEx.Errors.ToDictionary()
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetails = new
                {
                    Success = false,
                    Message = "An unexpected error occurred. Please try again later.",
                    context.Response.StatusCode,
                    Data = new { }
                };
                return context.Response.WriteAsync((string)JsonConvert.SerializeObject(errorDetails, jsonSettings));
        }
    }
}