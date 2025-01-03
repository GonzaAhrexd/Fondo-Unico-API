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
    public class ValoresController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ValoresController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Valores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valores>>> GetValores()
        {
            return await _context.Valores.ToListAsync();
        }

        // GET: api/Valores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Valores>> GetValores(int id)
        {
            var valores = await _context.Valores.FindAsync(id);

            if (valores == null)
            {
                return NotFound();
            }

            return valores;
        }

        // PUT: api/Valores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValores(int id, Valores valores)
        {
            if (id != valores.Id)
            {
                return BadRequest();
            }

            _context.Entry(valores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValoresExists(id))
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

        // POST: api/Valores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Valores>> PostValores(Valores valores)
        {
            _context.Valores.Add(valores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValores", new { id = valores.Id }, valores);
        }

        // DELETE: api/Valores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValores(int id)
        {
            var valores = await _context.Valores.FindAsync(id);
            if (valores == null)
            {
                return NotFound();
            }

            _context.Valores.Remove(valores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ValoresExists(int id)
        {
            return _context.Valores.Any(e => e.Id == id);
        }
    }
}
