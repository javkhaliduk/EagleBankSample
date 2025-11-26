using EagleBank.Models.APIException;
using Microsoft.AspNetCore.Diagnostics;

namespace EagleBank.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            ConfigureGlobalExceptionHandler(app);

            app.UseRateLimiter();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
        private static void ConfigureGlobalExceptionHandler(WebApplication app)
        {
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
        }
    }
}
