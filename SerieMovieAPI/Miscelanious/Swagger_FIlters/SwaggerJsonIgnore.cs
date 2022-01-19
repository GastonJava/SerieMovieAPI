using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;


namespace SerieMovieAPI.Miscelanious
{
    
    /*
    public class SwaggerJsonIgnore : IOperationFilter
    {
        
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                                 .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                                 );
            if (ignoredProperties.Any())
            {
                foreach (var property in ignoredProperties)
                {
                    operation.Parameters = operation.Parameters
                        .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture) && !p.In.Equals("route", StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                }

            }
        }
        
    }
    */

    /*
    public class SwaggerJsonIgnore : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var ignoredProperties = context.MethodInfo.GetParameters()
                .SelectMany(p => p.ParameterType.GetProperties()
                .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() != null));
            if (ignoredProperties.Any())
            {
                foreach (var property in ignoredProperties)
                {
                    
                 
                    operation.Parameters = operation.Parameters
                        .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture)
                        && !p.In.Equals("route", StringComparison.InvariantCultureIgnoreCase))
                        .ToList();
                }
            }
        }

    }
    */
}
