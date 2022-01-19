using Microsoft.OpenApi.Models;
using SerieMovieAPI.Miscelanious.Swagger_FIlters.Scheme_Filter;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace SerieMovieAPI.Miscelanious
{
    public class SwaggerSkipPropertyFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }

            var skipProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);

            foreach (var skipProperty in skipProperties)
            {
                var propertyToSkip = schema.Properties.Keys.SingleOrDefault(x => string.Equals(x, skipProperty.Name, StringComparison.OrdinalIgnoreCase));

                if (propertyToSkip != null)
                {
                    schema.Properties.Remove(propertyToSkip);
                }
            }
        }
    }
}
