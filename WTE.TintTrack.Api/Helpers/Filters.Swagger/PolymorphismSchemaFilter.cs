using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.DTOs.PropertySpecifications;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.PropertySpecifications;

namespace WTE.TintTrack.Api.Helpers.Filters.Swagger;

public class PolymorphismSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        /*// Check if the base class (Property) is being processed
        if (context.Type == typeof(PropertyDto) ||
            context.Type == typeof(Property))
        {
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "propertyType" // Matches your discriminator field
            };

            schema.Properties.Add("propertyType", new OpenApiSchema
            {
                Type = "string",
                Description = "Specifies the type of the property"
            });

            // Define derived types
            schema.OneOf = new List<OpenApiSchema>
            {
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(ArchitecturalProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(AutomotiveProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(ResidentialProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(CommercialProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(SpecialtyProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(GlassFilmProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(EnergyEfficientProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(CustomProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(SignageProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(OutdoorProperty) } },
                new OpenApiSchema { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(OtherProperty) } },
            };
        }*/


        // Check if the base class (PropertyDto) is being processed
        if (context.Type == typeof(PropertyDto))
        {
            // Set up the discriminator (only once, no need to add a property manually)
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "propertyType" // Matches your discriminator field
            };

            schema.Required = new HashSet<string> { "Type" }; // Ensure the discriminator property is required

            /*// Define the derived types in the 'oneOf' list
            schema.OneOf =
            [
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(ArchitecturalPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(AutomotivePropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(ResidentialPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(CommercialPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(SpecialtyPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(GlassFilmPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(EnergyEfficientPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(CustomPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(SignagePropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(OutdoorPropertyDto) } },
                new() { Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = nameof(OtherPropertyDto) } },
            ];*/

            schema.OneOf = [];
            var derivedTypes = new[] {
                typeof(ArchitecturalPropertyDto),
                typeof(AutomotivePropertyDto) ,
                typeof(ResidentialPropertyDto) ,
                typeof(CommercialPropertyDto) ,
                typeof(SpecialtyPropertyDto) ,
                typeof(GlassFilmPropertyDto) ,
                typeof(EnergyEfficientPropertyDto) ,
                typeof(CustomPropertyDto) ,
                typeof(SignagePropertyDto) ,
                typeof(OutdoorPropertyDto)
            };

            foreach (var derivedType in derivedTypes)
            {
                var derivedSchema = context.SchemaGenerator.GenerateSchema(derivedType, context.SchemaRepository);
                schema.OneOf.Add(derivedSchema);
            }
        }
    }
}