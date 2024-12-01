using Microsoft.AspNetCore.Mvc;
using TuringIA.Server.Context;
using TuringIA.Server.Models;
using System.Collections.Generic;
using System.Linq;
using TuringIA.Server.DTO;

namespace TuringIA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadosController : ControllerBase
    {
        private readonly TuringDbContext _context;

        public EstadosController(TuringDbContext context)
        {
            _context = context;
        }

        // GET: /api/estados
        [HttpGet]
        public ActionResult<IEnumerable<EstadoDTO>> GetEstados()
        {
            var estados = _context.Estados.Select(e => new EstadoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Abreviacion = e.Abreviacion,
                Fips = e.Fips
            }).ToList();

            return Ok(estados);
        }

        // GET: /api/estados/{id}
        [HttpGet("{id}")]
        public ActionResult<EstadoDTO> GetEstadoById(int id)
        {
            var estado = _context.Estados
                .Where(e => e.Id == id)
                .Select(e => new EstadoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Abreviacion = e.Abreviacion,
                    Fips = e.Fips
                })
                .FirstOrDefault();

            if (estado == null)
            {
                return NotFound(new { Message = "Estado no encontrado" });
            }

            return Ok(estado);
        }

        // POST: /api/estados
        [HttpPost]
        public ActionResult<EstadoDTO> CreateEstado([FromBody] EstadoDTO estadoDTO)
        {
            if (_context.Estados.Any(e => e.Nombre == estadoDTO.Nombre || e.Fips == estadoDTO.Fips))
            {
                return BadRequest(new { Message = "El estado ya existe o el FIPS está en uso" });
            }

            var nuevoEstado = new Estado
            {
                Nombre = estadoDTO.Nombre,
                Abreviacion = estadoDTO.Abreviacion,
                Fips = estadoDTO.Fips
            };

            _context.Estados.Add(nuevoEstado);
            _context.SaveChanges();

            estadoDTO.Id = nuevoEstado.Id;

            return CreatedAtAction(nameof(GetEstadoById), new { id = estadoDTO.Id }, estadoDTO);
        }

        // PUT: /api/estados/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEstado(int id, [FromBody] EstadoDTO estadoDTO)
        {
            var estadoExistente = _context.Estados.FirstOrDefault(e => e.Id == id);

            if (estadoExistente == null)
            {
                return NotFound(new { Message = "Estado no encontrado" });
            }

            estadoExistente.Nombre = estadoDTO.Nombre;
            estadoExistente.Abreviacion = estadoDTO.Abreviacion;
            estadoExistente.Fips = estadoDTO.Fips;

            _context.Estados.Update(estadoExistente);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /api/estados/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteEstado(int id)
        {
            var estado = _context.Estados.FirstOrDefault(e => e.Id == id);

            if (estado == null)
            {
                return NotFound(new { Message = "Estado no encontrado" });
            }

            _context.Estados.Remove(estado);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
