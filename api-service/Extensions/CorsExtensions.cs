namespace ApiService.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "AllowAll";

        public static IServiceCollection AddCorsPolicyAllowAll(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });
            return services;
        }

        public static IApplicationBuilder UseCorsPolicyAllowAll(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }
    }
}
