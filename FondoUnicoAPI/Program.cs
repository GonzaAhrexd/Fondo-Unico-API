using FondoUnicoAPI.Context;
using Microsoft.EntityFrameworkCore;

using dotenv.net;
using FondoUnicoAPI.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace FondoUnicoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Load();
            
            var builder = WebApplication.CreateBuilder(args);
            string cors = "http://localhost:4200";

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            // Toma SECRET_TOKEN_KEY desde el .env
            var secretKey = Environment.GetEnvironmentVariable("SECRET_TOKEN_KEY");

            builder.Services.AddAuthentication(options =>
            {
                // Configura la autenticación de cookies por defecto
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/api/Usuarios/login"; // Ruta donde se redirige si no está autenticado
                options.Cookie.Name = "AuthToken"; // Nombre de la cookie que estás usando
            });
            


            builder.Services.AddAuthorization();


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

            app.UseAuthentication();
            app.UseAuthorization();


 

            app.UseCors(cors);

            app.MapControllers();

            app.Run();
        }
    }
}
