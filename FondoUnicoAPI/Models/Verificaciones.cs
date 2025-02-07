using System.ComponentModel.DataAnnotations;

namespace FondoUnicoAPI.Models
{
    public class Verificaciones
    {

        [Key]
        public int Id { get; set; }
        public string Unidad { get; set; }
        public int Recibo { get; set; }
        public DateTime Fecha { get; set; }
        public string PlantaVerificadora { get; set; }
        public string Tipo { get; set; }
        public string Responsable { get; set; }
        public string Formulario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Anio { get; set; }
        public string Dominio { get; set; }
        public double Importe { get; set; }


    }
}
