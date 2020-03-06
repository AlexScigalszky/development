using System.Collections.Generic;
using EDAviation.Shared;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EDAviation.API.Filters
{
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.USER_TOKEN,
                Required = true,
                In = ParameterLocation.Header,
                Description = "Bearer access token",
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }
}
