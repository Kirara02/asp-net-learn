using System.Net;
using System.Text.Json;
using ApiService.Models.Common;

namespace ApiService.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;

        public ResponseWrapperMiddleware(RequestDelegate next, ILogger<ResponseWrapperMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memStream).ReadToEndAsync();
            memStream.Seek(0, SeekOrigin.Begin);

            context.Response.Body = originalBodyStream;

            // skip swagger and html
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Response.ContentType?.Contains("text/html") == true)
            {
                await context.Response.WriteAsync(responseBody);
                return;
            }

            // ✅ wrapping
            var status = context.Response.StatusCode;
            object wrapper;

            if (status >= 200 && status < 300)
            {
                wrapper = ApiResponse<object>.Ok(
                    TryDeserializeJson(responseBody),
                    status,
                    message: "Request completed successfully."
                );
            }
            else
            {
                var message = TryExtractErrorMessage(responseBody);

                if (string.IsNullOrWhiteSpace(message))
                {
                    message = status switch
                    {
                        400 => "Bad request.",
                        401 => "Unauthorized. Please provide a valid token.",
                        403 => "Forbidden. You don't have permission to access this resource.",
                        404 => "Resource not found.",
                        500 => "Internal server error.",
                        _ => $"Request failed with status code {status}."
                    };
                }

                wrapper = ApiResponse<object>.Fail(message, status);

            }

            var json = JsonSerializer.Serialize(
                wrapper,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    WriteIndented = true
                }
            );

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        private static object? TryDeserializeJson(string raw)
        {
            try
            {
                return JsonSerializer.Deserialize<object>(raw);
            }
            catch
            {
                return raw;
            }
        }

        private static string TryExtractErrorMessage(string raw)
        {
            try
            {
                using var doc = JsonDocument.Parse(raw);
                if (doc.RootElement.TryGetProperty("message", out var msg))
                    return msg.GetString() ?? "Error";
            }
            catch { }
            return raw;
        }
    }
}
