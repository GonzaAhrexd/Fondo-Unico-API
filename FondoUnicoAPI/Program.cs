using FondoUnicoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace FondoUnicoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string cors = "http://localhost:4200";

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Agregar los servicios a tu contenedor
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionBD") ?? throw new InvalidOperationException("Connection string 'FondoUnicoBD' not found.") ));

            builder.Services.AddCors(options => {
                options.AddPolicy(name: cors, builder =>
                {
                    builder.WithMethods("*");
                    builder.WithHeaders("*");
                    builder.WithOrigins("*");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(cors);

            app.MapControllers();

            app.Run();
        }
    }
}
