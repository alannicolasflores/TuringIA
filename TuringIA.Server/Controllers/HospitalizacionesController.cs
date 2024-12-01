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
    public class HospitalizacionesController : ControllerBase
    {
        private readonly TuringDbContext _context;

        public HospitalizacionesController(TuringDbContext context)
        {
            _context = context;
        }

        // GET: /api/hospitalizaciones
        [HttpGet]
        public ActionResult<IEnumerable<HospitalizacionDTO>> GetHospitalizaciones()
        {
            var hospitalizaciones = _context.Hospitalizaciones.Select(h => new HospitalizacionDTO
            {
                Id = h.Id,
                Fecha = h.Fecha,
                EstadoId = h.EstadoId,
                HospitalizadosActualmente = h.HospitalizadosActualmente,
                EnUciActualmente = h.EnUciActualmente,
                EnVentiladorActualmente = h.EnVentiladorActualmente,
                HospitalizacionesAcumuladas = h.HospitalizacionesAcumuladas
            }).ToList();

            return Ok(hospitalizaciones);
        }

        // GET: /api/hospitalizaciones/{id}
        [HttpGet("{id}")]
        public ActionResult<HospitalizacionDTO> GetHospitalizacionById(int id)
        {
            var hospitalizacion = _context.Hospitalizaciones
                .Where(h => h.Id == id)
                .Select(h => new HospitalizacionDTO
                {
                    Id = h.Id,
                    Fecha = h.Fecha,
                    EstadoId = h.EstadoId,
                    HospitalizadosActualmente = h.HospitalizadosActualmente,
                    EnUciActualmente = h.EnUciActualmente,
                    EnVentiladorActualmente = h.EnVentiladorActualmente,
                    HospitalizacionesAcumuladas = h.HospitalizacionesAcumuladas
                })
                .FirstOrDefault();

            if (hospitalizacion == null)
            {
                return NotFound(new { Message = "Hospitalización no encontrada" });
            }

            return Ok(hospitalizacion);
        }

        // GET: /api/hospitalizaciones/estado/{estadoId}
        [HttpGet("estado/{estadoId}")]
        public ActionResult<IEnumerable<HospitalizacionDTO>> GetHospitalizacionesByEstado(int estadoId)
        {
            var hospitalizaciones = _context.Hospitalizaciones
                .Where(h => h.EstadoId == estadoId)
                .Select(h => new HospitalizacionDTO
                {
                    Id = h.Id,
                    Fecha = h.Fecha,
                    EstadoId = h.EstadoId,
                    HospitalizadosActualmente = h.HospitalizadosActualmente,
                    EnUciActualmente = h.EnUciActualmente,
                    EnVentiladorActualmente = h.EnVentiladorActualmente,
                    HospitalizacionesAcumuladas = h.HospitalizacionesAcumuladas
                })
                .ToList();

            if (!hospitalizaciones.Any())
            {
                return NotFound(new { Message = "No se encontraron hospitalizaciones para este estado" });
            }

            return Ok(hospitalizaciones);
        }

        // POST: /api/hospitalizaciones
        [HttpPost]
        public ActionResult<HospitalizacionDTO> CreateHospitalizacion([FromBody] HospitalizacionDTO hospitalizacionDTO)
        {
            var nuevaHospitalizacion = new Hospitalizacione
            {
                Fecha = hospitalizacionDTO.Fecha,
                EstadoId = hospitalizacionDTO.EstadoId,
                HospitalizadosActualmente = hospitalizacionDTO.HospitalizadosActualmente,
                EnUciActualmente = hospitalizacionDTO.EnUciActualmente,
                EnVentiladorActualmente = hospitalizacionDTO.EnVentiladorActualmente,
                HospitalizacionesAcumuladas = hospitalizacionDTO.HospitalizacionesAcumuladas
            };

            _context.Hospitalizaciones.Add(nuevaHospitalizacion);
            _context.SaveChanges();

            hospitalizacionDTO.Id = nuevaHospitalizacion.Id;

            return CreatedAtAction(nameof(GetHospitalizacionById), new { id = hospitalizacionDTO.Id }, hospitalizacionDTO);
        }

        // PUT: /api/hospitalizaciones/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateHospitalizacion(int id, [FromBody] HospitalizacionDTO hospitalizacionDTO)
        {
            var hospitalizacionExistente = _context.Hospitalizaciones.FirstOrDefault(h => h.Id == id);

            if (hospitalizacionExistente == null)
            {
                return NotFound(new { Message = "Hospitalización no encontrada" });
            }

            hospitalizacionExistente.Fecha = hospitalizacionDTO.Fecha;
            hospitalizacionExistente.EstadoId = hospitalizacionDTO.EstadoId;
            hospitalizacionExistente.HospitalizadosActualmente = hospitalizacionDTO.HospitalizadosActualmente;
            hospitalizacionExistente.EnUciActualmente = hospitalizacionDTO.EnUciActualmente;
            hospitalizacionExistente.EnVentiladorActualmente = hospitalizacionDTO.EnVentiladorActualmente;
            hospitalizacionExistente.HospitalizacionesAcumuladas = hospitalizacionDTO.HospitalizacionesAcumuladas;

            _context.Hospitalizaciones.Update(hospitalizacionExistente);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /api/hospitalizaciones/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteHospitalizacion(int id)
        {
            var hospitalizacion = _context.Hospitalizaciones.FirstOrDefault(h => h.Id == id);

            if (hospitalizacion == null)
            {
                return NotFound(new { Message = "Hospitalización no encontrada" });
            }

            _context.Hospitalizaciones.Remove(hospitalizacion);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
