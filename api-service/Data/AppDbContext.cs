using ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Convert table & column names to snake_case
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()!));
            }
            
            // Optional: seed data default user admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "$2a$12$gzXHS7/t.nd76CXC96UmMuL4wrPqNACVvc76z0hWtPqskiB.lMjPq", // password hashed "admin"
                    Role = "Admin"
                }
            );
        }

        private static string ToSnakeCase(string name)
        {
            return string.Concat(
                name.Select((ch, i) =>
                    i > 0 && char.IsUpper(ch)
                        ? "_" + char.ToLowerInvariant(ch)
                        : char.ToLowerInvariant(ch).ToString())
            );
        }
    }
}