
//Classe che raccoglie i dati dei servizi del Program.cs
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
        {
            //Configurazione DB
            services.AddDbContext<DataContext>(opt =>
            {
                //Stringa di configurazione
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //Configurazione CORS
            services.AddCors();
            //Configurazione Token per l'accesso ai servizi HTTP
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}