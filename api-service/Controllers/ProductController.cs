using ApiService.Data;
using ApiService.Models;
using ApiService.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext _db;

        public ProductController(ILogger<ProductController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // 🔹 GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll()
        {
            var products = await _db.Products
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return Ok(products);
        }

        // 🔹 GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null)
                return NotFound(new { message = $"Product dengan id {id} tidak ditemukan." });

            return Ok(new ProductReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            });
        }

        // 🔹 POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Produk baru ditambahkan: {Name}", dto.Name);

            var result = new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CreatedAt = product.CreatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, result);
        }

        // 🔹 PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCreateDto dto)
        {
            var existing = await _db.Products.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Product dengan id {id} tidak ditemukan." });

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Description = dto.Description;

            await _db.SaveChangesAsync();
            _logger.LogInformation("Produk dengan ID {Id} berhasil diupdate", id);

            return NoContent();
        }

        // 🔹 DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = $"Product dengan id {id} tidak ditemukan." });

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Produk dengan ID {Id} dihapus", id);

            return NoContent();
        }
    }
}