using ApiService.Models;
using ApiService.Models.DTOs;

namespace ApiService.Services.Implementations
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto dto);
        Task<User?> ValidateUserAsync(LoginDto dto);
    }
}