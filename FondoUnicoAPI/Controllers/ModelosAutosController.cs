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
    public class ModelosAutosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ModelosAutosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ModelosAutos
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ModelosAutos>>> GetModelosAutos()
        {
            return await _context.ModelosAutos.ToListAsync();
        }

        // GET: api/ModelosAutos/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ModelosAutos>> GetModelosAutos(int id)
        {
            var modelosAutos = await _context.ModelosAutos.FindAsync(id);

            if (modelosAutos == null)
            {
                return NotFound();
            }

            return modelosAutos;
        }

        // PUT: api/ModelosAutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutModelosAutos(int id, ModelosAutos modelosAutos)
        {
            if (id != modelosAutos.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelosAutos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelosAutosExists(id))
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

        // POST: api/ModelosAutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{Marca}")]
        [Authorize]
        public async Task<ActionResult<ModelosAutos>> PostModelosAutos(string Marca, ModelosAutos modelosAutos)
        {
            // Obten el ID de Marca

            var marca = _context.MarcasAutos.Where(x => x.Marca == Marca).FirstOrDefault();
            int idMarca = marca.Id;

            // Ahora arma un objeto para enviar el Modelo en el body (modelosAutos) y el ID de idMarca

            modelosAutos.MarcaID = idMarca;

            _context.ModelosAutos.Add(modelosAutos);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetModelosAutos", new { id = modelosAutos.Id }, modelosAutos);
        }

        // DELETE: api/ModelosAutos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteModelosAutos(int id)
        {
            var modelosAutos = await _context.ModelosAutos.FindAsync(id);
            if (modelosAutos == null)
            {
                return NotFound();
            }

            _context.ModelosAutos.Remove(modelosAutos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelosAutosExists(int id)
        {
            return _context.ModelosAutos.Any(e => e.Id == id);
        }
    }
}
