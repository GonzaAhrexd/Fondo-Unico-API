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
    public class DepositosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DepositosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Depositos
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Deposito>>> GetDeposito()
        {
            return await _context.Deposito.ToListAsync();
        }

       

        [HttpGet("{unidad}/{fechaInicio}/{fechaFinal}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Deposito>>> GetDepositosPorUnidadFecha(string unidad, DateTime fechaInicio, DateTime fechaFinal)
        {
            if(unidad == "Listar todo")
            {
                // Retorna un arreglo con los depositos que cumplan con la fecha de inicio y final
                return await _context.Deposito.Where(e => e.Fecha >= fechaInicio && e.Fecha <= fechaFinal).ToListAsync();
            }
            // Retorna un arreglo con los depositos que cumplan con la unidad y la fecha de inicio y final
            return await _context.Deposito.Where(e => e.Unidad == unidad && e.Fecha >= fechaInicio && e.Fecha <= fechaFinal).ToListAsync();
        
        
        }



        // GET: api/Depositos/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Deposito>> GetDeposito(int id)
        {
            var deposito = await _context.Deposito.FindAsync(id);

            if (deposito == null)
            {
                return NotFound();
            }

            return deposito;
        }

        // PUT: api/Depositos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutDeposito(int id, Deposito deposito)
        {
            if (id != deposito.NroDeposito)
            {
                return BadRequest();
            }

            _context.Entry(deposito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepositoExists(id))
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

        // POST: api/Depositos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Deposito>> PostDeposito(Deposito deposito)
        {
            _context.Deposito.Add(deposito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeposito", new { id = deposito.NroDeposito }, deposito);
        }

        // DELETE: api/Depositos/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDeposito(int id)
        {
            var deposito = await _context.Deposito.FindAsync(id);
            if (deposito == null)
            {
                return NotFound();
            }

            _context.Deposito.Remove(deposito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepositoExists(int id)
        {
            return _context.Deposito.Any(e => e.NroDeposito == id);
        }
    }
}
