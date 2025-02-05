using FondoUnicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FondoUnicoAPI.Context
{
 


public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    // Define your DbSets here
        public DbSet<Bancos> Bancos { get; set; }
        public DbSet<Formularios> Formularios { get; set; }
        public DbSet<Tipos> Tipos { get; set; }
        public DbSet<RenglonesEntrega> RenglonesEntregas { get; set; }
        public DbSet<Entregas> Entregas { get; set; }
 
        public DbSet<FondoUnicoAPI.Models.Unidades> Unidades { get; set; } = default!;
 
        public DbSet<FondoUnicoAPI.Models.Deposito> Deposito { get; set; } = default!;
 
        public DbSet<FondoUnicoAPI.Models.Verificaciones> Verificaciones { get; set; } = default!;
 
        public DbSet<FondoUnicoAPI.Models.MarcasMotos> Marcas { get; set; } = default!;

        public DbSet<FondoUnicoAPI.Models.ModelosMotos> Modelos { get; set; } = default!;

        public DbSet<FondoUnicoAPI.Models.MarcasAutos> MarcasAutos { get; set; } = default!;
        public DbSet<FondoUnicoAPI.Models.ModelosAutos> ModelosAutos { get; set; } = default!;

    }
}