using ApiService.Models.Common;
using ApiService.Models.DTOs;
using ApiService.Models.DTOs.Common;
using ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // 🔹 GET: api/categories
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<CategoryDto>>>> GetPaged([FromQuery] QueryParamsDto query)
        {
            var paged = await _service.GetPagedAsync(query);
            return Ok(paged);
        }

        // 🔹 GET: api/categories/{id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
                return NotFound(new { message = $"Category with id {id} not found." });

            return Ok(category);
        }

        // 🔹 POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CategoryCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔹 PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = $"Category with id {id} not found." });

            return Ok(updated);
        }

        // 🔹 DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"Category with id {id} not found." });

            return Ok(new { message = $"Category with id {id} deleted successfully." });
        }
    }
}