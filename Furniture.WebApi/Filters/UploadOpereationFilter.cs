using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Furniture.WebApi.Filters;

public class UploadOpereationFilter: IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var formFileParameter = context.MethodInfo.GetParameters()
            .Any(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(List<IFormFile>));

        if (formFileParameter)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>()
                        }
                    }
                }
            };

            foreach (var param in context.MethodInfo.GetParameters())
            {
                if (param.ParameterType == typeof(IFormFile))
                {
                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(
                        param.Name,
                        new OpenApiSchema { Type = "string", Format = "binary" });
                }
                else if (param.ParameterType == typeof(List<IFormFile>))
                {
                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(
                        param.Name,
                        new OpenApiSchema
                        {
                            Type = "array",
                            Items = new OpenApiSchema { Type = "string", Format = "binary" }
                        });
                }
                else
                {
                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties.Add(
                        param.Name,
                        new OpenApiSchema { Type = "string" });
                }
            }
        }
    }
}