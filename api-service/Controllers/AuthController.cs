using ApiService.Models.DTOs;
using ApiService.Services.Implementations;
using ApiService.Services.Interfaces;
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
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return Ok(new { message = "User registered successfully.", username = user.Username });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role
                }
            });
        }
    }
    
}