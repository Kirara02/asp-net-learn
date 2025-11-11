using ApiService.Models.DTOs;
using ApiService.Models;
using ApiService.Repositories.Interfaces;
using ApiService.Services.Interfaces;
using AutoMapper;

namespace ApiService.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(entity);
        }

        public async Task<ProductReadDto?> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            // hanya field non-null yang diupdate (thanks to AutoMapper condition)
            _mapper.Map(dto, existing);

            _repository.Update(existing);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ProductReadDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _repository.Delete(existing);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
