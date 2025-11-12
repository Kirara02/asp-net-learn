using ApiService.Models.Common;
using ApiService.Models.DTOs;
using ApiService.Models.DTOs.Common;
using ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // 🔹 GET: api/products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<ProductReadDto>>>> GetPaged([FromQuery] QueryParamsDto query)
        {
            var paged = await _service.GetPagedAsync(query);
            return Ok(paged);
        }

        // 🔹 GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null)
                return NotFound(new { message = $"Product with id {id} not found." });

            return Ok(product);
        }

        // 🔹 POST: api/products
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> Create([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔹 PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductReadDto>> Update(int id, [FromBody] ProductUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Product with id {id} not found." });

            return Ok(updated);
        }

        // 🔹 DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Product with id {id} not found." });

            return NoContent();
        }
    }
}
