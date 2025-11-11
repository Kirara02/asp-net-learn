using Microsoft.AspNetCore.Mvc;

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
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var result = new
                    {
                        success = false,
                        message = "Validation failed",
                        errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}
