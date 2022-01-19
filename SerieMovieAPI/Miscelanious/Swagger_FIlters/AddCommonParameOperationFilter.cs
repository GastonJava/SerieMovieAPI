using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace SerieMovieAPI.Miscelanious
{
    public class AddCommonParameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null && !descriptor.ControllerName.StartsWith("Characters"))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Image_Characterbyte",
                    In = ParameterLocation.Query,
                    Description = "Character Images bytes",
                    Required = false
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Movieseries",
                    In = ParameterLocation.Query,
                    Description = "Movieserie list",
                    Required = false
                });

                
            }
        }
    }
}
