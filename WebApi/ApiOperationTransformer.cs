using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace WebApi;

public sealed class ApiOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var controllerName = context.Description.ActionDescriptor.RouteValues["controller"];
        var actionName = context.Description.ActionDescriptor.RouteValues["action"];

        operation.Tags = new List<OpenApiTag>
        {
            new() { Name = controllerName }
        };

        operation.Summary = $"[{controllerName}] → عملیات {actionName}";
        operation.Description = $"این متد مربوط به عملکرد `{actionName}` در کنترلر `{controllerName}` است.";

        return Task.CompletedTask;
    }
}
