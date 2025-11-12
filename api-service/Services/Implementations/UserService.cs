using System.Security.Cryptography;
using System.Text;
using ApiService.Models.Entities;
using ApiService.Models.DTOs;
using ApiService.Repositories.Interfaces;
using AutoMapper;

namespace ApiService.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            if (await _repository.GetByUsernameAsync(dto.Username) != null)
                throw new Exception("Username already taken.");

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

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> ValidateUserAsync(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash)) return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto?> UpdateAsync(int id, RegisterDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            user.Name = dto.Name ?? user.Name;
            user.Username = dto.Username ?? user.Username;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                user.PasswordSalt = hmac.Key;
            }

            _repository.Update(user);
            await _repository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            _repository.Delete(user);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}