using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WTE.TintTrack.Api.Helpers.Filters.Swagger;

public class GenericTypeDescriptionFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Get the declaring type and method info
        var methodInfo = context.MethodInfo;

        if (methodInfo.IsGenericMethod)
        {
            // Retrieve generic argument names
            var genericArgs = methodInfo.GetGenericArguments();
            var genericArgNames = string.Join(", ", genericArgs.Select(arg => arg.Name));

            // Update the operation summary to include the generic type name
            if (!string.IsNullOrEmpty(operation.Summary))
            {
                operation.Summary = operation.Summary.Replace("[TEntityDto]", genericArgNames);
            }

            // Optionally update description
            if (!string.IsNullOrEmpty(operation.Description))
            {
                operation.Description = operation.Description.Replace("[TEntityDto]", genericArgNames);
            }
        }
    }
}
