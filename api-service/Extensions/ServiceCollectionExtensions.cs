using ApiService.Repositories.Implementations;
using ApiService.Repositories.Interfaces;
using ApiService.Services.Implementations;
using ApiService.Services.Interfaces;

namespace ApiService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
