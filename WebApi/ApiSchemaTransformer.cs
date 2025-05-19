namespace WebApi;

using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

public sealed class ApiSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type.Name.EndsWith("Dto"))
        {
            schema.Description = $"مدلی برای {context.JsonTypeInfo.Type.Name} شامل داده‌های خروجی API.";
        }

        return Task.CompletedTask;
    }
}
