using FondoUnicoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public VehiclesController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // URL de la API
            string url = "https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getMakes&year=2022";
            _httpClient.Timeout = TimeSpan.FromSeconds(60);  // Aumenta el tiempo de espera

            // Realiza la solicitud GET a la API externa
            var response = await _httpClient.GetStringAsync(url);

            // Si es necesario, puedes procesar la respuesta, por ejemplo, eliminar el "callback" para obtener un JSON válido
            // Asumiendo que la respuesta es un JSON, se eliminaría el "callback=?"
            string jsonResponse = response.Substring(response.IndexOf('(') + 1, response.LastIndexOf(')') - response.IndexOf('(') - 1);



            // Retorna los datos como JSON
            return Ok(jsonResponse);
        }
        catch(Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
    // Haz un get por marcas
    [HttpGet("{make}")]
    public async Task<IActionResult> Get(string make)
    {
        try
        {
            // URL de la API
            string url = $"https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getModels&make={make}";
            _httpClient.Timeout = TimeSpan.FromSeconds(60);  // Aumenta el tiempo de espera

            // Realiza la solicitud GET a la API externa
            var response = await _httpClient.GetStringAsync(url);

            // Si es necesario, puedes procesar la respuesta, por ejemplo, eliminar el "callback" para obtener un JSON válido
            // Asumiendo que la respuesta es un JSON, se eliminaría el "callback=?"
            string jsonResponse = response.Substring(response.IndexOf('(') + 1, response.LastIndexOf(')') - response.IndexOf('(') - 1);

            // Retorna los datos como JSON
            return Ok(jsonResponse);
        }
        catch(Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // Trae los datos https://www.carqueryapi.com/api/0.3/?callback=?&cmd=getMakes&year=2022 y pasalos a la BD en la tabla MarcasAutos
   

    [HttpGet("motorcycles")]
    public async Task<IActionResult> GetMotorcycles()
    {
        try
        {
           // Importa los datos del archivo .json (moto_brands.json) y muestralos
           var json = System.IO.File.ReadAllText("moto_brands.json");
            return Ok(json);



            
        }
        catch(Exception ex)
        {
            // En caso de error, se retorna el mensaje de error
            return BadRequest($"Error: {ex.Message}");
        }
    }

    public class BrandList
    {
        public List<Brand> Data { get; set; }
    }

    public class ModelsList
    {
        public List<Model> Data { get; set; }
    }


    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Model
    {
        public int brand_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    // Get motorcycle/:make para vincular los modelos con las marcas buscando de el json moto_models
    [HttpGet("motorcycles/{make}")]
    public async Task<IActionResult> GetMotorcycles(string make)
    {
        try
        {
            var brandsJson = System.IO.File.ReadAllText("moto_brands.json");
            var modelsJson = System.IO.File.ReadAllText("moto_models.json");

            // Deserializar JSON a objetos
            var brands = JsonConvert.DeserializeObject<Dictionary<string, List<Brand>>>(brandsJson);
            var models = JsonConvert.DeserializeObject<Dictionary<string, List<Model>>>(modelsJson);

            // Asegúrate de que 'brands' y 'models' tienen la estructura correcta
            if(!brands.ContainsKey("data") || !models.ContainsKey("data"))
            {
                return BadRequest("Estructura de JSON incorrecta.");
            }

            // Encuentra la marca por el nombre proporcionado
            var brand = brands["data"].FirstOrDefault(b => b.name.Equals(make, StringComparison.OrdinalIgnoreCase));

            if(brand == null)
            {
                return NotFound($"La marca '{make}' no fue encontrada.");
            }

            // Filtra los modelos que corresponden a la marca encontrada
            var brandModels = models["data"]
                .Where(m => m.brand_id == brand.id)
                .Select(m => new { m.id, m.name })
                .ToList();

            return Ok(brandModels);
        }
        catch(Exception ex)
        {
            // En caso de error, se retorna el mensaje de error
            return BadRequest($"Error: {ex.Message}");
        }
    }



}
