using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FondoUnicoAPI.Context;
using FondoUnicoAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace FondoUnicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificacionesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VerificacionesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Verificaciones
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Verificaciones>>> GetVerificaciones()
        {
            return await _context.Verificaciones.Where(e => e.estaActivo == true).ToListAsync();

        }

        // Haz un get por Unidad y Fecha 
        [HttpGet("{unidad}/{fecha}")]
        //[Authorize]
        public async Task<IActionResult> Get(string unidad, DateTime fecha)
        {
            var verificaciones = await _context.Verificaciones
                                               .Where(x => x.Unidad == unidad && x.Fecha == fecha && x.estaActivo == true)
                                               .ToListAsync();

            if(verificaciones == null )
            {
                return NotFound(); // Devuelve un 404 si no se encuentra ninguna verificación
            }

            return Ok(verificaciones); // Devuelve un 200 OK con los resultados
        }

        [HttpGet("{unidad}/{desde}/{hasta}/{tipo}")]
        [Authorize]
        public async Task<IActionResult> Get(string unidad, DateTime desde, DateTime hasta, string tipo)
        {
            var query = _context.Verificaciones
                                .Where(x => x.Unidad == unidad && x.Fecha >= desde && x.Fecha <= hasta && x.estaActivo == true);

            if(!string.IsNullOrEmpty(tipo) && tipo != "no_ingresado")
            {
                query = query.Where(x => x.Tipo == tipo);
            }

            var verificaciones = await query.ToListAsync();

            if(!verificaciones.Any())
            {
                return NotFound(); // Devuelve un 404 si no se encuentra ninguna verificación
            }

            return Ok(verificaciones); // Devuelve un 200 OK con los resultados
        }
        [HttpGet("buscar-por-verificacion/{recibo}")]
        [Authorize]
        public async Task<IActionResult> Get(int recibo)
        {
            var verificaciones = await _context.Verificaciones
                                               .Where(x => x.Recibo == recibo && x.estaActivo == true)
                                               .ToListAsync();

            if(verificaciones == null || !verificaciones.Any())
            {
                return NotFound(); // Devuelve un 404 si no se encuentra ninguna verificación
            }

            return Ok(verificaciones); // Devuelve un 200 OK con los resultados
        }

        // GET: api/Verificaciones/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Verificaciones>> GetVerificaciones(int id)
        {
            var verificaciones = await _context.Verificaciones.FindAsync(id);

            if(verificaciones == null || !verificaciones.estaActivo)    
            {
                return NotFound(); // Devuelve 404 si no existe o si EstaActivo es false
            }

            return verificaciones; // Devuelve el objeto solo si EstaActivo es true
        }

        // PUT: api/Verificaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutVerificaciones(int id, Verificaciones verificaciones)
        {
            verificaciones.estaActivo = true;
            if (id != verificaciones.Id)
            {
                return BadRequest();
            }

            _context.Entry(verificaciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Verificaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Verificaciones>> PostVerificaciones(Verificaciones verificaciones)
        {
            verificaciones.estaActivo = true;

            _context.Verificaciones.Add(verificaciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVerificaciones", new { id = verificaciones.Id }, verificaciones);
        }

        // DELETE: api/Verificaciones/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVerificaciones(int id)
        {
            var verificaciones = await _context.Verificaciones.FindAsync(id);
            if (verificaciones == null)
            {
                return NotFound();
            }


            verificaciones.estaActivo = false;
            _context.Verificaciones.Update(verificaciones);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private bool VerificacionesExists(int id)
        {
            return _context.Verificaciones.Any(e => e.Id == id);
        }
    }
}
