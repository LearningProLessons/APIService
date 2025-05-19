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

        operation.Summary = $"[{controllerName}] → Operation {actionName}";
        operation.Description = $"This method corresponds to the `{actionName}` action in the `{controllerName}` controller.";

        return Task.CompletedTask;
    }

}
