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
    public class CasosDiariosController : ControllerBase
    {
        private readonly TuringDbContext _context;

        public CasosDiariosController(TuringDbContext context)
        {
            _context = context;
        }

        // GET: /api/casosdiarios
        [HttpGet]
        public ActionResult<IEnumerable<CasoDiarioDTO>> GetCasosDiarios()
        {
            var casosDiarios = _context.CasosDiarios.Select(c => new CasoDiarioDTO
            {
                Id = c.Id,
                Fecha = c.Fecha,
                EstadoId = c.EstadoId,
                CondadoId = c.CondadoId,
                Casos = c.Casos,
                Muertes = c.Muertes
            }).ToList();

            return Ok(casosDiarios);
        }

        // GET: /api/casosdiarios/{id}
        [HttpGet("{id}")]
        public ActionResult<CasoDiarioDTO> GetCasoDiarioById(int id)
        {
            var casoDiario = _context.CasosDiarios
                .Where(c => c.Id == id)
                .Select(c => new CasoDiarioDTO
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    EstadoId = c.EstadoId,
                    CondadoId = c.CondadoId,
                    Casos = c.Casos,
                    Muertes = c.Muertes
                })
                .FirstOrDefault();

            if (casoDiario == null)
            {
                return NotFound(new { Message = "Caso diario no encontrado" });
            }

            return Ok(casoDiario);
        }

        // GET: /api/casosdiarios/estado/{estadoId}
        [HttpGet("estado/{estadoId}")]
        public ActionResult<IEnumerable<CasoDiarioDTO>> GetCasosDiariosByEstado(int estadoId)
        {
            var casosDiarios = _context.CasosDiarios
                .Where(c => c.EstadoId == estadoId)
                .Select(c => new CasoDiarioDTO
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    EstadoId = c.EstadoId,
                    CondadoId = c.CondadoId,
                    Casos = c.Casos,
                    Muertes = c.Muertes
                })
                .ToList();

            if (!casosDiarios.Any())
            {
                return NotFound(new { Message = "No se encontraron casos diarios para este estado" });
            }

            return Ok(casosDiarios);
        }

        // GET: /api/casosdiarios/condado/{condadoId}
        [HttpGet("condado/{condadoId}")]
        public ActionResult<IEnumerable<CasoDiarioDTO>> GetCasosDiariosByCondado(int condadoId)
        {
            var casosDiarios = _context.CasosDiarios
                .Where(c => c.CondadoId == condadoId)
                .Select(c => new CasoDiarioDTO
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    EstadoId = c.EstadoId,
                    CondadoId = c.CondadoId,
                    Casos = c.Casos,
                    Muertes = c.Muertes
                })
                .ToList();

            if (!casosDiarios.Any())
            {
                return NotFound(new { Message = "No se encontraron casos diarios para este condado" });
            }

            return Ok(casosDiarios);
        }

        // POST: /api/casosdiarios
        [HttpPost]
        public ActionResult<CasoDiarioDTO> CreateCasoDiario([FromBody] CasoDiarioDTO casoDiarioDTO)
        {
            var nuevoCasoDiario = new CasosDiario
            {
                Fecha = casoDiarioDTO.Fecha,
                EstadoId = casoDiarioDTO.EstadoId,
                CondadoId = casoDiarioDTO.CondadoId,
                Casos = casoDiarioDTO.Casos,
                Muertes = casoDiarioDTO.Muertes
            };

            _context.CasosDiarios.Add(nuevoCasoDiario);
            _context.SaveChanges();

            casoDiarioDTO.Id = nuevoCasoDiario.Id;

            return CreatedAtAction(nameof(GetCasoDiarioById), new { id = casoDiarioDTO.Id }, casoDiarioDTO);
        }

        // PUT: /api/casosdiarios/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCasoDiario(int id, [FromBody] CasoDiarioDTO casoDiarioDTO)
        {
            var casoExistente = _context.CasosDiarios.FirstOrDefault(c => c.Id == id);

            if (casoExistente == null)
            {
                return NotFound(new { Message = "Caso diario no encontrado" });
            }

            casoExistente.Fecha = casoDiarioDTO.Fecha;
            casoExistente.EstadoId = casoDiarioDTO.EstadoId;
            casoExistente.CondadoId = casoDiarioDTO.CondadoId;
            casoExistente.Casos = casoDiarioDTO.Casos;
            casoExistente.Muertes = casoDiarioDTO.Muertes;

            _context.CasosDiarios.Update(casoExistente);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /api/casosdiarios/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCasoDiario(int id)
        {
            var casoDiario = _context.CasosDiarios.FirstOrDefault(c => c.Id == id);

            if (casoDiario == null)
            {
                return NotFound(new { Message = "Caso diario no encontrado" });
            }

            _context.CasosDiarios.Remove(casoDiario);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
