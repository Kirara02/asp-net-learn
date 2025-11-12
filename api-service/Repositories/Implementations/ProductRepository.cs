using ApiService.Data;
using ApiService.Models.Entities;
using ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Products
            .Include(p => p.Category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        public async Task<Product?> GetByIdAsync(int id) => await _db.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        public async Task AddAsync(Product product) => await _db.Products.AddAsync(product);
        public void Update(Product product) => _db.Products.Update(product);
        public void Delete(Product product) => _db.Products.Remove(product);
        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();

        public async Task<(IEnumerable<Product> Items, int Total)> GetPagedAsync(int page, int limit, string? search,  int? categoryId = null)
        {
            var query = _db.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value); 

            var total = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return (items, total);
        }
    }
}