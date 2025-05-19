using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace WebApi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
 

public sealed class ApiDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "FinBridge Payment API Documentation",
            Version = "v1",
            Description = "This documentation covers all operations related to transactions, clinics, accounts, and more.",
            Contact = new OpenApiContact
            {
                Name = "FinBridge Development Team",
                Email = "dev@finbridge.ir",
                Url = new Uri("https://finbridge.ir/support")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        document.Tags = new List<OpenApiTag>
        {
            new() { Name = "Transactions", Description = "Management and operations of transactions" },
            new() { Name = "Clinics", Description = "Management of clinics" },
            new() { Name = "Accounts", Description = "Therapists' bank accounts" },
            new() { Name = "Weathers", Description = "Weather information" },
        };


        return Task.CompletedTask;
    }
}

