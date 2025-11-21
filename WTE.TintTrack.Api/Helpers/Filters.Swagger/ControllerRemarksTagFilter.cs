using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WTE.TintTrack.Api.Helpers.Filters.Swagger;

/// <summary>
/// Filter to include controller remarks in Swagger tag descriptions
/// </summary>
public class ControllerRemarksTagFilter : IDocumentFilter
{
    private readonly XDocument? _xmlComments;

    public ControllerRemarksTagFilter()
    {
        // Try multiple paths to find the XML documentation file
        var possiblePaths = new[]
        {
            Path.Combine(AppContext.BaseDirectory, "WTE.TintTrack.Api.xml"),
            Path.Combine(Directory.GetCurrentDirectory(), "bin", "Debug", "net9.0", "WTE.TintTrack.Api.xml"),
            Path.Combine(Directory.GetCurrentDirectory(), "bin", "Release", "net9.0", "WTE.TintTrack.Api.xml"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WTE.TintTrack.Api.xml")
        };

        foreach (var xmlPath in possiblePaths)
        {
            if (File.Exists(xmlPath))
            {
                try
                {
                    _xmlComments = XDocument.Load(xmlPath);
                    break;
                }
                catch
                {
                    // Continue to next path if this one fails
                }
            }
        }
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (_xmlComments == null)
        {
            // XML file not found - this is expected in some deployment scenarios
            // but remarks won't be included if XML is missing
            return;
        }

        // Build a dictionary of controller types to their XML documentation
        var controllerDocs = new Dictionary<string, (string? Summary, string? Remarks)>();

        // Process each API description to find controllers and their XML docs
        var controllerTypes = context.ApiDescriptions
            .Select(apiDesc => apiDesc.ActionDescriptor)
            .OfType<ControllerActionDescriptor>()
            .Select(desc => desc.ControllerTypeInfo.AsType())
            .Distinct()
            .ToList();

        foreach (var controllerType in controllerTypes)
        {
            // Get the full type name with namespace (XML format: T:Namespace.TypeName)
            var fullTypeName = $"T:{controllerType.FullName}";
            
            // Find the member element in XML documentation
            var memberElement = _xmlComments.Descendants("member")
                .FirstOrDefault(m => m.Attribute("name")?.Value == fullTypeName);

            if (memberElement != null)
            {
                var summary = memberElement.Element("summary")?.Value?.Trim();
                var remarks = memberElement.Element("remarks")?.Value?.Trim();
                
                // Store by controller name (without "Controller" suffix)
                var controllerName = controllerType.Name.Replace("Controller", string.Empty);
                controllerDocs[controllerName] = (summary, remarks);
            }
        }

        // First, ensure all tags referenced in operations exist in swaggerDoc.Tags
        var existingTagNames = new HashSet<string>(swaggerDoc.Tags.Select(t => t.Name));
        
        foreach (var pathItem in swaggerDoc.Paths.Values)
        {
            // OpenApiPathItem uses Operations dictionary, not direct properties
            foreach (var operation in pathItem.Operations.Values)
            {
                if (operation?.Tags == null)
                    continue;

                foreach (var tagRef in operation.Tags)
                {
                    if (!existingTagNames.Contains(tagRef.Name))
                    {
                        var newTag = new OpenApiTag { Name = tagRef.Name };
                        swaggerDoc.Tags.Add(newTag);
                        existingTagNames.Add(tagRef.Name);
                    }
                }
            }
        }

        // Now update all tags with remarks
        foreach (var tag in swaggerDoc.Tags)
        {
            if (controllerDocs.TryGetValue(tag.Name, out var docs))
            {
                var descriptionParts = new List<string>();
                
                // Determine the summary text (from existing description or XML)
                string? summaryText = null;
                if (!string.IsNullOrEmpty(tag.Description))
                {
                    // If Swashbuckle already set description from summary, use it
                    summaryText = tag.Description;
                }
                else if (!string.IsNullOrEmpty(docs.Summary))
                {
                    summaryText = docs.Summary;
                }
                
                // Format summary as bold using Markdown
                if (!string.IsNullOrEmpty(summaryText))
                {
                    descriptionParts.Add($"**{summaryText}**");
                }
                
                // Always append remarks if they exist (not bold)
                if (!string.IsNullOrEmpty(docs.Remarks))
                {
                    descriptionParts.Add(docs.Remarks);
                }
                
                // Set the combined description
                if (descriptionParts.Count > 0)
                {
                    tag.Description = string.Join("\n\n", descriptionParts);
                }
            }
        }
    }
}

