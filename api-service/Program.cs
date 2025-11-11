using ApiService.Extensions;
using Serilog;

namespace ApiService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("Logs/api.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
                
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services
                .AddDatabase(builder.Configuration)
                .AddAppServices()
                .AddAutoMapper(typeof(AutoMapperProfile))
                .AddJsonSnakeCase()
                .AddSwaggerWithJwt();

            var app = builder.Build();

            app.ApplyMigrations();

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
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => "Hello from Kirara's .NET API 👋");
            app.MapControllers();

            app.Run();
        }
    }
}
