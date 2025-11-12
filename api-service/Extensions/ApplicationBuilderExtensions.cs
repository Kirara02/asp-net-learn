using ApiService.Middleware;
using Serilog;

namespace ApiService.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalMiddlewares(this IApplicationBuilder app)
        {
            // ✅ 1. Logging request dengan Serilog
            app.UseSerilogRequestLogging();

            // ✅ 2. CORS Policy
            app.UseCorsPolicyAllowAll();

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ResponseWrapperMiddleware>();


            // ✅ 4. Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // ✅ 5. Redirect HTTP → HTTPS (optional)
            app.UseHttpsRedirection();

            return app;
        }
    }
}
