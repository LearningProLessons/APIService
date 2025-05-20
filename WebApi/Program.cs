using Microsoft.IdentityModel.Tokens;
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


builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";

        options.RequireHttpsMetadata = true;
        options.Audience = "charity_api";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:5001",

            ValidateAudience = true,
            ValidAudience = "charity_api",

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),

            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireTenant", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("tenant_id");
    });

    options.AddPolicy("SuperAdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("role", "SuperAdmin");
    });

    options.AddPolicy("CharityAccess", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("role", "SuperAdmin") ||
            (context.User.HasClaim("scope", "charity.client.access") &&
             context.User.HasClaim(c => c.Type == "tenant_id"))
        );
    });

    options.AddPolicy("CampaignManager", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("role", "SuperAdmin") ||
            (context.User.HasClaim("role", "CampaignManager") &&
             context.User.HasClaim("scope", "charity.campaign.manage"))
        );
    });

    options.AddPolicy("FinanceManager", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("role", "SuperAdmin") ||
            (context.User.HasClaim("role", "FinanceManager") &&
             context.User.HasClaim("scope", "charity.finance.manage"))
        );
    });

    options.AddPolicy("BranchReader", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("role", "SuperAdmin") ||
            (context.User.HasClaim("role", "BranchManager") &&
             context.User.HasClaim("scope", "charity.branch.read"))
        );
    });

    options.AddPolicy("BranchWriter", policy =>
    {
        policy.RequireAssertion(context =>
            context.User.HasClaim("role", "SuperAdmin") ||
            (context.User.HasClaim("role", "BranchManager") &&
             context.User.HasClaim("scope", "charity.branch.write"))
        );
    });

    options.AddPolicy("TenantPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("tenant_id");
    });
});


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "OpenApiDemo"); });

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("Title tab")
        .WithDarkMode(true)
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http);
});

app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-store";
    context.Response.Headers["Pragma"] = "no-cache";
    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();  
}


app.Run();