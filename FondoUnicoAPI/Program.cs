using FondoUnicoAPI.Context;
using Microsoft.EntityFrameworkCore;

using dotenv.net;
using FondoUnicoAPI.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


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
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            // Obtener SECRET_TOKEN_KEY desde la configuración
            var secretKey = builder.Configuration["SECRET_TOKEN_KEY"];

            Console.WriteLine($"Valor de SECRET_TOKEN_KEY: {secretKey}");

            if(string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("SECRET_TOKEN_KEY no está definido en las variables de entorno.");
            }

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
            {
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
                
                opt.RequireHttpsMetadata = false;

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = signingKey
                };
                
            });

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

            builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()  // Agrega variables de entorno
    .AddIniFile(".env", optional: true, reloadOnChange: true);  // Esto solo si quieres usar un .env, si no, omítelo


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
