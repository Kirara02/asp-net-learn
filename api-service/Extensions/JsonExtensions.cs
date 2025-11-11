using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiService.Extensions
{
    public static class JsonExtensions
    {
        public static IServiceCollection AddJsonSnakeCase(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
            });
            return services;
        }
    }
}
