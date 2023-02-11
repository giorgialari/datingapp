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
            //Configurazione del contesto di dati per l'utilizzo del database
            services.AddDbContext<DataContext>(opt =>
            {
                //Usa la stringa di configurazione "DefaultConnection" per configurare l'accesso al database Sqlite
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            //Abilita la configurazione CORS per consentire richieste da origini diverse
            services.AddCors();
            //Registra il servizio ITokenService per la generazione e la verifica dei token di accesso
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
