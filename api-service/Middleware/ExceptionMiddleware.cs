using System.Net;
using System.Text.Json;

namespace ApiService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env; // ✅ tambahkan ini

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env; // ✅ simpan environment
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🔥 Unhandled exception occurred!");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorBody = new
                {
                    message = ex.Message,
                    details = _env.IsDevelopment() ? ex.StackTrace : null // ✅ gunakan _env
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorBody, options));
            }
        }
    }
}
