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
    public class MarcasMotosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MarcasMotosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/MarcasMotos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcasMotos>>> GetMarcas()
        {
            var Marcas = await _context.Marcas.ToListAsync();

            // Ordena alfabeticamente

            Marcas = Marcas.OrderBy(x => x.Marca).ToList();

            return Marcas;


        }

        // GET: api/MarcasMotos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcasMotos>> GetMarcasMotos(int id)
        {
            var marcasMotos = await _context.Marcas.FindAsync(id);

            if (marcasMotos == null)
            {
                return NotFound();
            }

            return marcasMotos;
        }
        [HttpGet("Modelos/{marca}")]
        public async Task<ActionResult<IEnumerable<ModelosMotos>>> GetModelosAutos(string marca)
        {
            var marcaId = _context.Marcas.Where(m => m.Marca == marca).FirstOrDefault().Id;


            var modelosAutos = await _context.Modelos.Where(m => m.MarcaID == marcaId).ToListAsync();
            modelosAutos = modelosAutos.OrderBy(x => x.Modelo).ToList();


            if(modelosAutos == null)
            {
                return NotFound();
            }
            Console.WriteLine(modelosAutos);

            return modelosAutos;
        }



        // PUT: api/MarcasMotos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcasMotos(int id, MarcasMotos marcasMotos)
        {
            if (id != marcasMotos.Id)
            {
                return BadRequest();
            }

            _context.Entry(marcasMotos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcasMotosExists(id))
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

        // POST: api/MarcasMotos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MarcasMotos>> PostMarcasMotos(MarcasMotos marcasMotos)
        {
            _context.Marcas.Add(marcasMotos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarcasMotos", new { id = marcasMotos.Id }, marcasMotos);
        }

        // DELETE: api/MarcasMotos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarcasMotos(int id)
        {
            var marcasMotos = await _context.Marcas.FindAsync(id);
            if (marcasMotos == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marcasMotos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcasMotosExists(int id)
        {
            return _context.Marcas.Any(e => e.Id == id);
        }
    }
}
