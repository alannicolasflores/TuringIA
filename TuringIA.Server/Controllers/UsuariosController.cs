using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TuringIA.Server.Context;

using TuringIA.Server.Models;
using System.Text;
using TuringIA.Server.DTO;

namespace TuringIA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly TuringDbContext _context;

        public UsuariosController(TuringDbContext context)
        {
            _context = context;
        }

        // GET /api/usuarios
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Rol = u.Rol,
                    FechaCreacion = u.FechaCreacion
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET /api/usuarios/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            return Ok(new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol,
                FechaCreacion = usuario.FechaCreacion
            });
        }

        // PUT /api/usuarios/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioRegistroDTO registroDTO)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var isAdmin = User.IsInRole("admin");

            if (userIdClaim == null || (!isAdmin && userIdClaim != usuario.Id.ToString()))
                return Forbid();

            usuario.Nombre = registroDTO.Nombre ?? usuario.Nombre;
            usuario.Email = registroDTO.Email ?? usuario.Email;

            if (!string.IsNullOrWhiteSpace(registroDTO.Contraseña))
                usuario.Contrasea = HashPassword(registroDTO.Contraseña);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario actualizado exitosamente." });
        }

        // DELETE /api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Usuario eliminado exitosamente." });
        }

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
