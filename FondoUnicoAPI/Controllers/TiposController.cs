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
    public class TiposController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TiposController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Tipos
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tipos>>> GetTipos()
        {
            return await _context.Tipos.ToListAsync();
        }

        // GET: api/Tipos/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Tipos>> GetTipos(int id)
        {
            var tipos = await _context.Tipos.FindAsync(id);

            if (tipos == null)
            {
                return NotFound();
            }

            return tipos;
        }

        // PUT: api/Tipos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTipos(int id, Tipos tipos)
        {
            if (id != tipos.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposExists(id))
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

        // POST: api/Tipos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Tipos>> PostTipos(Tipos tipos)
        {
            _context.Tipos.Add(tipos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipos", new { id = tipos.Id }, tipos);
        }

        // DELETE: api/Tipos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTipos(int id)
        {
            var tipos = await _context.Tipos.FindAsync(id);
            if (tipos == null)
            {
                return NotFound();
            }

            _context.Tipos.Remove(tipos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiposExists(int id)
        {
            return _context.Tipos.Any(e => e.Id == id);
        }
    }
}
