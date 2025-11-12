using ApiService.Models.Entities;

namespace ApiService.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task SaveChangesAsync();

        Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(int page, int limit, string? search);
    }
}