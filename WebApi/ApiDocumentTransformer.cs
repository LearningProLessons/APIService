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
            Title = "مستندات API پرداخت FinBridge",
            Version = "v1",
            Description = "این مستندات شامل تمام عملیات مرتبط با تراکنش‌ها، کلینیک‌ها، حساب‌ها و ... است.",
            Contact = new OpenApiContact
            {
                Name = "تیم توسعه فین‌بریج",
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
            new() { Name = "Transactions", Description = "مدیریت و عملیات تراکنش‌ها" },
            new() { Name = "Clinics", Description = "مدیریت کلینیک‌ها" },
            new() { Name = "Accounts", Description = "حساب‌های بانکی درمانگران" },
            new() { Name = "Weathers", Description = "آب و هوا" },
        };

        return Task.CompletedTask;
    }
}

