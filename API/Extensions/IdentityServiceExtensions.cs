//Classe che estende la configurazione delle funzionalità di autenticazione per l'applicazione
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        //Metodo per la configurazione dei servizi di autenticazione
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {
            //Configurazione Autenticazione ai metodi con il Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //Regole di validazione del token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Viene controllata la chiave di firma del token e si assicurerà che si valida in base a quella dell'emittente (l'emittente è il servizio API)
                    ValidateIssuerSigningKey = true,
                    //Viene specificata qual è la chiave d'accesso dell'emittente (TokenKey è lo stesso che c'è in TokenService.cs)
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    //Disabilita la validazione dell'emittente
                    ValidateIssuer = false,
                    //Disabilita la validazione del destinatario
                    ValidateAudience = false
                };

            });
            //ritorna i servizi configurati
            return services;
        }
    }

}