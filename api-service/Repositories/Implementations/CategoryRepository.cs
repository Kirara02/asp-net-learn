using ApiService.Data;
using ApiService.Models.Entities;
using ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _db.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) => await _db.Categories.FindAsync(id);

        public async Task AddAsync(Category category) => await _db.Categories.AddAsync(category);

        public void Update(Category category) => _db.Categories.Update(category);

        public void Delete(Category category) => _db.Categories.Remove(category);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(int page, int limit, string? search)
        {
            var query = _db.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()));

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
    }
}