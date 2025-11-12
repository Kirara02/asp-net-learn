using System.Security.Cryptography;
using System.Text;
using ApiService.Data;
using ApiService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Extensions
{
    public static class DatabaseExtensions
    {
        // Register DbContext
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
            );
            return services;
        }

        // Migrate + optional seed in one method
        public static void ApplyMigrationsAndSeed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Apply migration
            db.Database.Migrate();

            // Seed default admin
            if (!db.Users.Any(u => u.Username == "admin"))
            {
                using var hmac = new HMACSHA512();
                var admin = new User
                {
                    Name = "Admin",
                    Username = "admin",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")),
                    PasswordSalt = hmac.Key,
                    Role = "Admin"
                };
                db.Users.Add(admin);
                db.SaveChanges();
                Console.WriteLine("👑 Default admin created (username: admin, password: admin)");
            }
        }
    }
}
