using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FondoUnicoAPI.Context;
using FondoUnicoAPI.Models;

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
        public async Task<ActionResult<IEnumerable<Verificaciones>>> GetVerificaciones()
        {
            return await _context.Verificaciones.ToListAsync();
        }

        // GET: api/Verificaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Verificaciones>> GetVerificaciones(int id)
        {
            var verificaciones = await _context.Verificaciones.FindAsync(id);

            if (verificaciones == null)
            {
                return NotFound();
            }

            return verificaciones;
        }

        // PUT: api/Verificaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVerificaciones(int id, Verificaciones verificaciones)
        {
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
        public async Task<ActionResult<Verificaciones>> PostVerificaciones(Verificaciones verificaciones)
        {
            _context.Verificaciones.Add(verificaciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVerificaciones", new { id = verificaciones.Id }, verificaciones);
        }

        // DELETE: api/Verificaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVerificaciones(int id)
        {
            var verificaciones = await _context.Verificaciones.FindAsync(id);
            if (verificaciones == null)
            {
                return NotFound();
            }

            _context.Verificaciones.Remove(verificaciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificacionesExists(int id)
        {
            return _context.Verificaciones.Any(e => e.Id == id);
        }
    }
}
