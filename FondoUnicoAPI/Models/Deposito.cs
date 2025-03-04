using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FondoUnicoAPI.Models
{
    public class Deposito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configura como auto incremental    
        public int NroDeposito { get; set; }
        public int NroTicket { get; set; }
        public DateTime Fecha { get; set; }
        public string Unidad { get; set; }
        public string Banco { get; set; }
        public string Cuenta { get; set; }
        public string Tipo { get; set; }
        public float Importe { get; set; }
    }
}
