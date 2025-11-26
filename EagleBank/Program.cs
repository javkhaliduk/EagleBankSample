using EagleBank.code;
using EagleBank.filters;
using EagleBank.Models.APIException;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Repository.Implementation;
using EagleBank.Repository.Interfaces;
using EagleBank.Repository.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomValidationFilter>();
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserDetailsOrchestrator, UserDetailsOrchestrator>();
builder.Services.AddScoped<IBankAccountOrchestrator, BankAccountOrchestrator>();
builder.Services.AddScoped<ITransactionOrchestrator, TransactionOrchestrator>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();
builder.Services.AddSingleton<ICacheService>(provider =>
{
    var cacheDuration = builder.Configuration.GetValue<int>("CacheDurationMinutes");
    return new CacheService(TimeSpan.FromMinutes(cacheDuration));
});
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();
    if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings?.Secret))
    {
        throw new Exception("Missing JWT Settings");
    }
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings?.ValidIssuer,
        ValidAudience = jwtSettings?.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? ""))
    };
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
         RateLimitPartition.GetFixedWindowLimiter(
             partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
             factory: partition => new FixedWindowRateLimiterOptions
             {
                 AutoReplenishment = true,
                 PermitLimit = 10,
                 QueueLimit = 0,
                 Window = TimeSpan.FromMinutes(1)
             }));
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = feature?.Error;

        int statusCode = 500;
        string message = "An unexpected error occurred";

        if (ex is ApiErrorException apiEx)
        {
            statusCode = apiEx.StatusCode;
            message = apiEx.Message;
            if (statusCode >= 500)
                logger.LogError(ex, "Server error: {Message}", message);
            else
                logger.LogWarning("Client error: {StatusCode} - {Message}", statusCode, message);
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { message = message });
    });
});

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
