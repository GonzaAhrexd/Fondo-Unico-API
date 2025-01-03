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
    public class EntregasController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public EntregasController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Entregas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entregas>>> GetEntregas()
        {
           return await _context.Entregas.Include(e => e.RenglonesEntregas).ToListAsync();
           
        }

        // GET: api/Entregas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entregas>> GetEntregas(int id)
        {
            var entregas = await _context.Entregas.FindAsync(id);
            // Haz que traiga la entrega con sus renglones
            entregas = await _context.Entregas.Include(e => e.RenglonesEntregas).FirstOrDefaultAsync(e => e.NroEntrega == id);
            if (entregas == null)
            {
                return NotFound();
            }

            return entregas;
        }

        // PUT: api/Entregas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntregas(int id, Entregas entregas)
        {
            if (id != entregas.NroEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entregas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregasExists(id))
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

        // POST: api/Entregas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entregas>> PostEntregas(Entregas entregas)
        {
            _context.Entregas.Add(entregas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntregas", new { id = entregas.NroEntrega }, entregas);
        }

        // DELETE: api/Entregas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntregas(int id)
        {
            var entregas = await _context.Entregas.FindAsync(id);
            if (entregas == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entregas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregasExists(int id)
        {
            return _context.Entregas.Any(e => e.NroEntrega == id);
        }
    }
}
