using ApiService.Models.Entities;

namespace ApiService.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}