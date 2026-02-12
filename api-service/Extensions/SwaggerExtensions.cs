using Microsoft.OpenApi;

namespace ApiService.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kirara API",
                    Version = "v1",
                    Description = "A clean .NET 10 Web API using PostgreSQL, JWT Authentication, and Serilog logging.",
                    Contact = new OpenApiContact
                    {
                        Name = "Kirara Bernstein",
                        Url = new Uri("https://fathul-portfolio.netlify.app/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                var jwtScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token"
                };

                c.AddSecurityDefinition("Bearer", jwtScheme);

                c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer"),
                        new List<string>()
                    }
                });

            });

            return services;
        }
    }
}