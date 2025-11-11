using System.Security.Cryptography;
using System.Text;
using ApiService.Data;
using ApiService.Extensions;
using ApiService.Models;
using Microsoft.EntityFrameworkCore;
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
                .AddRepositories()
                .AddDomainServices()
                .AddJwtAuthentication(builder.Configuration)
                .AddAutoMapper(typeof(AutoMapperProfile))
                .AddJsonSnakeCase()
                .AddSwaggerWithJwt()
                .AddCorsPolicyAllowAll();


            var app = builder.Build();

            app.ApplyMigrationsAndSeed();

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
            app.UseCorsPolicyAllowAll();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => "Hello from Kirara's .NET API 👋");
            app.MapControllers();

            app.Run();
        }
    }
}
