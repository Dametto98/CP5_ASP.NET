using Microsoft.AspNetCore.Mvc;
using SafeScribe.API.DTOs;
using SafeScribe.API.Models;
using SafeScribe.API.Services;

namespace SafeScribe.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")] 
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        // Lista em memória para simular um anco de dados de usuários
        private static readonly List<User> _users = new();

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("registrar")] // Endpoint: POST /api/v1/auth/registrar
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

        [HttpPost("login")] // Endpoint: POST /api/v1/auth/login
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
    }
}