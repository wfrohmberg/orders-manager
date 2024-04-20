using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;

namespace OrdersManager.Swagger
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        //public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        //{
        //    if (operation.parameters == null)
        //        operation.parameters = new List<Parameter>();

        //    operation.parameters.Add(new Parameter
        //    {
        //        name = "MyHeaderField",
        //        @in = "header",
        //        type = "string",
        //        description = "My header field",
        //        required = true
        //    });
        //}

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "token",
                In = ParameterLocation.Header
            });
        }
    }
}
