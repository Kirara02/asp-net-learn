using ApiService.Models.DTOs;

namespace ApiService.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}