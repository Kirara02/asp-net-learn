using ApiService.Models.Common;
using ApiService.Models.DTOs;
using ApiService.Models.DTOs.Common;

namespace ApiService.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryDto?> UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto);
        Task<bool> DeleteAsync(int id);

        Task<PagedResponse<CategoryDto>> GetPagedAsync(QueryParamsDto query);
    }
}