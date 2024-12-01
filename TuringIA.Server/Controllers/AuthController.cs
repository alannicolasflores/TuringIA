using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TuringIA.Server.Context;
using TuringIA.Server.DTO;

using TuringIA.Server.Models;

namespace TuringIA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TuringDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(TuringDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (usuario == null || !VerificarContraseña(loginDTO.Contraseña, usuario.Contrasea))
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            var token = GenerarToken(usuario);

            return Ok(new TokenDTO
            {
                AccessToken = token,
                RefreshToken = "static-refresh-token", // Sustituir con lógica real para refresh tokens
                ExpiraEn = 10800 // 3 horas
            });
        }

        // POST /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegistroDTO registroDTO)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == registroDTO.Email))
            {
                return BadRequest(new { message = "El correo electrónico ya está en uso." });
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = registroDTO.Nombre,
                Email = registroDTO.Email,
                Contrasea = HashPassword(registroDTO.Contraseña),
                Rol = registroDTO.Rol ?? "usuario",
                FechaCreacion = DateTime.UtcNow
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado exitosamente." });
        }

        // POST /api/auth/refresh-token
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            // Implementar validación y generación de un nuevo access token.
            return Ok(new TokenDTO
            {
                AccessToken = "new-access-token",
                RefreshToken = "new-refresh-token",
                ExpiraEn = 10800
            });
        }

        // POST /api/auth/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Implementar lógica para invalidar tokens si se usa.
            return Ok(new { message = "Sesión cerrada exitosamente." });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerificarContraseña(string input, string hashedPassword)
        {
            return HashPassword(input) == hashedPassword;
        }
    }
}
