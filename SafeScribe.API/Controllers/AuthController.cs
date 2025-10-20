using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeScribe.API.DTOs;
using SafeScribe.API.Models;
using SafeScribe.API.Services;
using System.Security.Claims; 
using System.IdentityModel.Tokens.Jwt; 

namespace SafeScribe.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITokenBlacklistService _blacklistService;
        private static readonly List<User> _users = new();

        public AuthController(ITokenService tokenService, ITokenBlacklistService blacklistService)
        {
            _tokenService = tokenService;
            _blacklistService = blacklistService; 
        }

        [HttpPost("registrar")]
        public IActionResult Register([FromBody] UserRegisterDto registerDto)
        {
            if (_users.Any(u => u.Username == registerDto.Username))
            {
                return BadRequest("Nome de usuário já existe.");
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new User
            {
                Id = _users.Count + 1,
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                Role = registerDto.Role
            };
            _users.Add(newUser);
            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            var user = _users.FirstOrDefault(u => u.Username == loginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Unauthorized("Credenciais inválidas.");
            }
            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            if (jti != null)
            {
                await _blacklistService.AddToBlacklistAsync(jti);
            }
            return Ok(new { message = "Logout realizado com sucesso." });
        }
    }
}