using System.Text.Json;
using ApiService.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiService.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ResponseWrapperMiddleware(
            RequestDelegate next,
            ILogger<ResponseWrapperMiddleware> logger,
            IOptions<JsonOptions> jsonOptions)
        {
            _next = next;
            _logger = logger;
            _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            await using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(memStream).ReadToEndAsync();
            memStream.Seek(0, SeekOrigin.Begin);
            context.Response.Body = originalBody;

            // skip swagger/html
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Response.ContentType?.Contains("text/html") == true)
            {
                await context.Response.WriteAsync(bodyText);
                return;
            }

            if (string.IsNullOrWhiteSpace(bodyText))
                bodyText = "{}";

            if (bodyText.Contains("\"success\":") && bodyText.Contains("\"error\":"))
            {
                await context.Response.WriteAsync(bodyText);
                return;
            }

            var statusCode = context.Response.StatusCode;
            object result;
            var correlationId = context.Items["CorrelationId"]?.ToString();

            try
            {
                if (statusCode >= 200 && statusCode < 300)
                {
                    var data = TryDeserialize(bodyText);
                    result = ApiResponse<object>.Ok(data, statusCode);
                }
                else
                {
                    string message = statusCode switch
                    {
                        400 => "Bad request.",
                        401 => "Unauthorized. Please provide a valid token.",
                        403 => "Forbidden. You don't have permission to access this resource.",
                        404 => "Resource not found.",
                        500 => "Internal server error.",
                        _ => "Request failed."
                    };

                    object? details = null;

                    try
                    {
                        using var doc = JsonDocument.Parse(bodyText);
                        if (doc.RootElement.TryGetProperty("message", out var msgProp))
                            message = msgProp.GetString() ?? message;
                        if (doc.RootElement.TryGetProperty("error", out var errProp) &&
                            errProp.TryGetProperty("details", out var detailsProp))
                            details = JsonSerializer.Deserialize<object>(detailsProp.GetRawText());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "⚠️ Failed to parse error response body.");
                    }

                    var enhancedDetails = new
                    {
                        path = context.Request.Path,
                        timestamp = DateTime.UtcNow,
                        correlation_id = correlationId,
                        info = details
                    };

                    result = ApiResponse<object>.Fail(message, statusCode, enhancedDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🔥 Failed to wrap response.");
                result = ApiResponse<object>.Fail("Response wrapping error.", 500, new
                {
                    correlation_id = correlationId,
                    error = ex.Message
                });
            }

            var json = JsonSerializer.Serialize(result, _jsonOptions);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        private static object? TryDeserialize(string raw)
        {
            try { return JsonSerializer.Deserialize<object>(raw); }
            catch { return raw; }
        }
    }
}
