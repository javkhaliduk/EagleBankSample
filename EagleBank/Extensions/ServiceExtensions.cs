using EagleBank.code;
using EagleBank.filters;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Repository.Implementation;
using EagleBank.Repository.Interfaces;
using EagleBank.Repository.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace EagleBank.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
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

            ConfigureOrchestrators(builder);

            ConfigureRepositories(builder);

            builder.Services.AddScoped<IJWTGenerator, JWTGenerator>();

            builder.Services.AddSingleton<ICacheService>(provider =>
            {
                var cacheDuration = builder.Configuration.GetValue<int>("CacheDurationMinutes");
                return new CacheService(TimeSpan.FromMinutes(cacheDuration));
            });

            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection(nameof(JWTSettings)));

            ConfigureAuthentication(builder);

            ConfigureRateLimiter(builder);

        }
        private static void ConfigureRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

        }
        private static void ConfigureOrchestrators(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserDetailsOrchestrator, UserDetailsOrchestrator>();
            builder.Services.AddScoped<IBankAccountOrchestrator, BankAccountOrchestrator>();
            builder.Services.AddScoped<ITransactionOrchestrator, TransactionOrchestrator>();
        }
        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
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
        }
        private static void ConfigureRateLimiter(WebApplicationBuilder builder)
        {
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
        }
    }
}
