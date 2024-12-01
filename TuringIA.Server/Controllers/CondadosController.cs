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
    public class CondadosController : ControllerBase
    {
        private readonly TuringDbContext _context;

        public CondadosController(TuringDbContext context)
        {
            _context = context;
        }

        // GET: /api/condados
        [HttpGet]
        public ActionResult<IEnumerable<CondadoDTO>> GetCondados()
        {
            var condados = _context.Condados.Select(c => new CondadoDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                EstadoId = c.EstadoId,
                Fips = c.Fips
            }).ToList();

            return Ok(condados);
        }

        // GET: /api/condados/{id}
        [HttpGet("{id}")]
        public ActionResult<CondadoDTO> GetCondadoById(int id)
        {
            var condado = _context.Condados
                .Where(c => c.Id == id)
                .Select(c => new CondadoDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    EstadoId = c.EstadoId,
                    Fips = c.Fips
                })
                .FirstOrDefault();

            if (condado == null)
            {
                return NotFound(new { Message = "Condado no encontrado" });
            }

            return Ok(condado);
        }

        // POST: /api/condados
        [HttpPost]
        public ActionResult<CondadoDTO> CreateCondado([FromBody] CondadoDTO condadoDTO)
        {
            if (_context.Condados.Any(c => c.Nombre == condadoDTO.Nombre || c.Fips == condadoDTO.Fips))
            {
                return BadRequest(new { Message = "El condado ya existe o el FIPS está en uso" });
            }

            var nuevoCondado = new Condado
            {
                Nombre = condadoDTO.Nombre,
                EstadoId = condadoDTO.EstadoId,
                Fips = condadoDTO.Fips
            };

            _context.Condados.Add(nuevoCondado);
            _context.SaveChanges();

            condadoDTO.Id = nuevoCondado.Id;

            return CreatedAtAction(nameof(GetCondadoById), new { id = condadoDTO.Id }, condadoDTO);
        }

        // PUT: /api/condados/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCondado(int id, [FromBody] CondadoDTO condadoDTO)
        {
            var condadoExistente = _context.Condados.FirstOrDefault(c => c.Id == id);

            if (condadoExistente == null)
            {
                return NotFound(new { Message = "Condado no encontrado" });
            }

            condadoExistente.Nombre = condadoDTO.Nombre;
            condadoExistente.EstadoId = condadoDTO.EstadoId;
            condadoExistente.Fips = condadoDTO.Fips;

            _context.Condados.Update(condadoExistente);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /api/condados/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCondado(int id)
        {
            var condado = _context.Condados.FirstOrDefault(c => c.Id == id);

            if (condado == null)
            {
                return NotFound(new { Message = "Condado no encontrado" });
            }

            _context.Condados.Remove(condado);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
