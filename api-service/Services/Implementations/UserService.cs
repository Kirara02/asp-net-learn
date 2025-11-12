using System.Security.Cryptography;
using System.Text;
using ApiService.Models.Entities;
using ApiService.Models.DTOs;
using ApiService.Repositories.Interfaces;

namespace ApiService.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> RegisterAsync(RegisterDto dto)
        {
            if (await _repository.GetByUsernameAsync(dto.Username) != null)
            {
                throw new Exception("Username already taken.");
            }

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return user;
        }

        public async Task<User?> ValidateUserAsync(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash)) return null;

            return user;
        }
    }
}