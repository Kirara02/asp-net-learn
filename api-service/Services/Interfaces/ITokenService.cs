using ApiService.Models;

namespace ApiService.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}