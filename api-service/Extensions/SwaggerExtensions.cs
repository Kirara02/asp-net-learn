using Microsoft.OpenApi.Models;

namespace ApiService.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // 📘 Basic API Info
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kirara API",
                    Version = "v1",
                    Description = "A clean .NET 9 Web API using PostgreSQL, JWT Authentication, and Serilog logging.",
                    Contact = new OpenApiContact
                    {
                        Name = "Kirara Bernstein",
                        Url = new Uri("https://github.com/your-github-or-portfolio"),
                        Email = "youremail@example.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // 🔐 JWT Bearer Authentication Setup
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token below using the format:\n\n**Bearer {your_token}**"
                });

                // 📋 Global Security Requirement (applied to all endpoints)
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // ✅ Optional: include XML comments (if enabled)
                // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }
    }
}
