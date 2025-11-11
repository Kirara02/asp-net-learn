using ApiService.Repositories.Implementations;
using ApiService.Repositories.Interfaces;

namespace ApiService.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
