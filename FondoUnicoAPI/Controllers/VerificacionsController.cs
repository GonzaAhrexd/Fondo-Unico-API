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
    public class VerificacionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VerificacionsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Verificacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Verificacion>>> GetVerificacion()
        {
            return await _context.Verificacion.ToListAsync();
        }

        // GET: api/Verificacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Verificacion>> GetVerificacion(int id)
        {
            var verificacion = await _context.Verificacion.FindAsync(id);

            if (verificacion == null)
            {
                return NotFound();
            }

            return verificacion;
        }

        // PUT: api/Verificacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVerificacion(int id, Verificacion verificacion)
        {
            if (id != verificacion.Recibo)
            {
                return BadRequest();
            }

            _context.Entry(verificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificacionExists(id))
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

        // POST: api/Verificacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Verificacion>> PostVerificacion(Verificacion verificacion)
        {
            _context.Verificacion.Add(verificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVerificacion", new { id = verificacion.Recibo }, verificacion);
        }

        // DELETE: api/Verificacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVerificacion(int id)
        {
            var verificacion = await _context.Verificacion.FindAsync(id);
            if (verificacion == null)
            {
                return NotFound();
            }

            _context.Verificacion.Remove(verificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VerificacionExists(int id)
        {
            return _context.Verificacion.Any(e => e.Recibo == id);
        }
    }
}
