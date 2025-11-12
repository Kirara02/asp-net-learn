using Microsoft.AspNetCore.Mvc;
using ApiService.Models.Common;
using System.Text.RegularExpressions;

namespace ApiService.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection AddValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>()
                        .CreateLogger("ModelValidation");

                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => ToSnakeCase(kvp.Key),
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    logger.LogWarning("Validation failed: {@Errors}", errors);

                    var response = ApiResponse<object>.Fail("Validation failed", 400, errors);

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
