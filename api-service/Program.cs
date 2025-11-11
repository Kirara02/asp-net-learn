using System.Text.Json;
using System.Text.Json.Serialization;
using ApiService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // builder setup
            var builder = WebApplication.CreateBuilder(args);

            // Add postgres database context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );


            // tambahkan service (jika nanti perlu controller, dbcontext, dll)
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // 🔹 Set naming policy ke snake_case
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;

                // 🔹 (Opsional) Biar enum juga jadi string
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower));
            });
            
            // Add swagger services (optional)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Kirara API",
                    Version = "v1",
                    Description = "Belajar .NET API dengan PostgreSQL dan JWT"
                });

                // Menambahkan support JWT di Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Masukkan token JWT dengan format: Bearer {token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

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
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            // 🔹 6. Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kirara API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            // 🔹 7. Route test
            app.MapGet("/", () => "Hello from Kirara's .NET API 👋");

            // 🔹 8. Map controllers (nanti dipakai untuk CRUD)
            app.MapControllers();

            app.Run();
        }
    }
}