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
    public class ModelosMotosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ModelosMotosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/ModelosMotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelosMotos>>> GetModelos()
        {
            return await _context.Modelos.ToListAsync();
        }

        // GET: api/ModelosMotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelosMotos>> GetModelosMotos(int id)
        {
            var modelosMotos = await _context.Modelos.FindAsync(id);

            if (modelosMotos == null)
            {
                return NotFound();
            }

            return modelosMotos;
        }

        // PUT: api/ModelosMotos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelosMotos(int id, ModelosMotos modelosMotos)
        {
            if (id != modelosMotos.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelosMotos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelosMotosExists(id))
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

        // POST: api/ModelosMotos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelosMotos>> PostModelosMotos(ModelosMotos modelosMotos)
        {
            _context.Modelos.Add(modelosMotos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelosMotos", new { id = modelosMotos.Id }, modelosMotos);
        }
        [HttpPost("{Marca}")]

        public async Task<ActionResult<ModelosMotos>> PostModelosMotos(string Marca, ModelosMotos modelosMotos)
        {
            // Obten el ID de Marca

            var marca = _context.Marcas.Where(x => x.Marca == Marca).FirstOrDefault();
            int idMarca = marca.Id;


            // Ahora arma un objeto para enviar el Modelo en el body (modelosAutos) y el ID de idMarca

            modelosMotos.MarcaID = idMarca; 
    
            _context.Modelos.Add( modelosMotos);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetModelosMotos", new { id = modelosMotos.Id }, modelosMotos);
        }



        // DELETE: api/ModelosMotos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelosMotos(int id)
        {
            var modelosMotos = await _context.Modelos.FindAsync(id);
            if (modelosMotos == null)
            {
                return NotFound();
            }

            _context.Modelos.Remove(modelosMotos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelosMotosExists(int id)
        {
            return _context.Modelos.Any(e => e.Id == id);
        }
    }
}
