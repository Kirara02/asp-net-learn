using ApiService.Models.Common;
using ApiService.Models.DTOs;
using ApiService.Models.DTOs.Common;

namespace ApiService.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllAsync();
        Task<ProductReadDto?> GetByIdAsync(int id);
        Task<ProductReadDto> CreateAsync(ProductCreateDto dto);
        Task<ProductReadDto?> UpdateAsync(int id, ProductUpdateDto dto);
        Task<bool> DeleteAsync(int id);

        Task<PagedResponse<ProductReadDto>> GetPagedAsync(QueryParamsDto query);
    }
}