namespace ApiService.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationHeader = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Reuse correlation ID dari request header kalau ada
            var correlationId = context.Request.Headers[CorrelationHeader].FirstOrDefault()
                                ?? Guid.NewGuid().ToString();

            context.Response.Headers[CorrelationHeader] = correlationId;
            context.Items["CorrelationId"] = correlationId;

            await _next(context);
        }
    }
}
