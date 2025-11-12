using System.Security.Claims;
using ApiService.Models.DTOs;
using ApiService.Services.Implementations;
using ApiService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await _userService.RegisterAsync(dto);
            return Ok(new { message = "User registered successfully.", data = user });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var user = await _userService.ValidateUserAsync(dto);
            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            var token = _tokenService.GenerateToken(user);
            return Ok(new AuthResponseDto
            {
                Token = token,
                User = user
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "User ID claim not found." });

            var userId = int.Parse(userIdClaim);

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found." });

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Role = user.Role
            };

            return Ok(userDto);
        }
    }
}