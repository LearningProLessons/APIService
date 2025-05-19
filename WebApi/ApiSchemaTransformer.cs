namespace WebApi;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

public sealed class ApiSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type.Name.EndsWith("Dto"))
        {
            schema.Description = $"A model for {context.JsonTypeInfo.Type.Name} containing API response data.";
        }

        return Task.CompletedTask;
    }

}
