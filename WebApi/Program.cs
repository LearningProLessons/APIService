using Microsoft.OpenApi;
using Scalar.AspNetCore;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi("openapi", options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
 
    
    options.AddDocumentTransformer<ApiDocumentTransformer>();
    options.AddOperationTransformer<ApiOperationTransformer>();
    options.AddSchemaTransformer<ApiSchemaTransformer>();
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerUI(options => 
{
    options.SwaggerEndpoint("/openapi/v1.json", "OpenApiDemo");
    // options.InjectStylesheet("/swagger/swagger-custom.css");
});

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Title tab")
        .WithDarkMode(true)
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http);
});

app.Run();