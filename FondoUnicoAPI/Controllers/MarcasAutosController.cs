using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FondoUnicoAPI.Context;
using FondoUnicoAPI.Models;
using Newtonsoft.Json;

namespace FondoUnicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasAutosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MarcasAutosController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/MarcasAutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcasAutos>>> GetMarcasAutos()
        {
            return await _context.MarcasAutos.ToListAsync();
        }

        // GET: api/MarcasAutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcasAutos>> GetMarcasAutos(int id)
        {
            var marcasAutos = await _context.MarcasAutos.FindAsync(id);

            if (marcasAutos == null)
            {
                return NotFound();
            }

            return marcasAutos;
        }
        // Haz que ingresando una marca me devuelva los modelos de autos de esa marca buscando por ID en la tabla de ModelosAutos, es decir, te paso una marca, extraes el ID y lo buscas en modelos de auto
        [HttpGet("Modelos/{marca}")]
        public async Task<ActionResult<IEnumerable<ModelosAutos>>> GetModelosAutos(string marca)
        {
            var marcaId = _context.MarcasAutos.Where(m => m.Marca == marca).FirstOrDefault().Id;
            var modelosAutos = await _context.ModelosAutos.Where(m => m.MarcaID == marcaId).ToListAsync();

            if (modelosAutos == null)
            {
                return NotFound();
            }
            Console.WriteLine(modelosAutos);

            return modelosAutos;
        }

        [HttpPost("Modelos")]
        public async Task<ActionResult<MarcasAutos>> PostModelosAutos(String marcasAutos)
        {
            // Busca en la base de Datos una Marca con el nombre ingresado en marcasAutos y extrae el ID

            var marca = _context.MarcasAutos.Where(x => x.Marca == marcasAutos).FirstOrDefault();
            int idMarca = marca.Id;

            // Ahora toma los datos del API https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getModels&make=MARCA remplazando marca por lo que se ingrese en marcasAutos y devuelve este json

            string url = "https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getModels&make=" + marcasAutos;
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(60);  // Aumenta el tiempo de espera
            httpClient.BaseAddress = new Uri(url);
            // Realiza la solicitud GET a la API externa
            var response = await httpClient.GetStringAsync(url);

            string jsonResponse = response.Substring(response.IndexOf('(') + 1, response.LastIndexOf(')') - response.IndexOf('(') - 1);
            // Ahora, convierte a un array de objetos donde solo tenga make_display de cada uno 
            var data = JsonConvert.DeserializeObject<ApiResponseModels>(jsonResponse);
            List<string> modelos = new List<string>();

            foreach(var item in data.Models)
            {
                modelos.Add(item.model_name);
            }

            foreach(var item in modelos)
            {
                ModelosAutos modelo = new ModelosAutos();
                modelo.Modelo = item;
                modelo.MarcaID = idMarca;

                _context.ModelosAutos.Add(modelo);
            }

            _context.SaveChanges();



            return Ok(modelos);
        }

        public class ApiResponseModels
        {
            public List<Model> Models { get; set; }
            
        }

        public class Model
        {
            public string model_name { get; set; }
        }


        // PUT: api/MarcasAutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarcasAutos(int id, MarcasAutos marcasAutos)
        {
            if (id != marcasAutos.Id)
            {
                return BadRequest();
            }

            _context.Entry(marcasAutos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcasAutosExists(id))
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

        // POST: api/MarcasAutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MarcasAutos>> PostMarcasAutos(MarcasAutos marcasAutos)
        {
            _context.MarcasAutos.Add(marcasAutos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarcasAutos", new { id = marcasAutos.Id }, marcasAutos);
        }

        // DELETE: api/MarcasAutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarcasAutos(int id)
        {
            var marcasAutos = await _context.MarcasAutos.FindAsync(id);
            if (marcasAutos == null)
            {
                return NotFound();
            }

            _context.MarcasAutos.Remove(marcasAutos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarcasAutosExists(int id)
        {
            return _context.MarcasAutos.Any(e => e.Id == id);
        }


         
    }
}
