using ApiService.Models.Common;
using ApiService.Models.DTOs;
using ApiService.Models.DTOs.Common;
using ApiService.Models.Entities;
using ApiService.Repositories.Interfaces;
using ApiService.Services.Interfaces;
using AutoMapper;

namespace ApiService.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> UpdateAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(categoryUpdateDto, existing);
            await _categoryRepository.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _categoryRepository.Delete(existing);
            await _categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<PagedResponse<CategoryDto>> GetPagedAsync(QueryParamsDto query)
        {
            var (items, total) = await _categoryRepository.GetPagedAsync(query.Page, query.Limit, query.Search);
            var dtoItems = _mapper.Map<IEnumerable<CategoryDto>>(items);

            return new PagedResponse<CategoryDto>
            {
                Items = dtoItems,
                TotalItems = total,
                Page = query.Page,
                Limit = query.Limit
            };
        }
    }
}