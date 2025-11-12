using ApiService.Models.Entities;

namespace ApiService.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
        Task SaveChangesAsync();

        Task<(IEnumerable<Product> Items, int Total)> GetPagedAsync(int page, int limit, string? search,  int? categoryId = null);

    }
}