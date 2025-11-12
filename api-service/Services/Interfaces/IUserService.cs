using ApiService.Models.Entities;
using ApiService.Models.DTOs;

namespace ApiService.Services.Implementations
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterDto dto);
        Task<UserDto?> ValidateUserAsync(LoginDto dto);
        Task<UserDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<UserDto?> UpdateAsync(int id, RegisterDto dto);
    }
}