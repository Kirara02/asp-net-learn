using System.Net;
using System.Text.Json;
using ApiService.Models.Common;

namespace ApiService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var correlationId = context.Items["CorrelationId"]?.ToString();
                _logger.LogError(ex, "🔥 Unhandled exception caught! CorrelationId: {CorrelationId}", correlationId);

                var status = (int)HttpStatusCode.InternalServerError;
                var details = new
                {
                    correlation_id = correlationId,
                    stack_trace = _env.IsDevelopment() ? ex.StackTrace : null
                };

                var response = ApiResponse<object>.Fail(
                    message: "Internal server error.",
                    status: status,
                    details: details
                );

                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = true
                }));
            }
        }
    }
}
