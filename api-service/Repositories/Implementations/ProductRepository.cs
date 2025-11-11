using System.Reflection.Metadata.Ecma335;
using ApiService.Data;
using ApiService.Models;
using ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);
        public void Update(Product product) => _context.Products.Update(product);
        public void Delete(Product product) => _context.Products.Remove(product);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}