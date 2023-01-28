

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //Configurazione token di sicurezza
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //Qui viene indicato al JwtRegisteredClaimNames quale valore deve abbinare al NameId presente al suo interno
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)

            };
            //Firma per salvare le credenziali come nuove, il primo parametro è la chiave, 
            //il secondo è l'algoritmo che viene usato per criptarla
            //Ovvero, crea un set di credenziali per firmare il token, utilizzando la chiave simmetrica e l'algoritmo HmacSha512
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Descrive il token da creare, con l'identità del soggetto, la data di scadenza e le credenziali di firma
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            //Utilizzo di JwtSecurityTokenHandler per creare e scrivere il token
            //Gestore di sicurezza del Token
            var tokenHandler = new JwtSecurityTokenHandler();

            //Salva il gestore dei token per creare un nuovo metodo CreateToken
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //restituisce il token, prima lo scrive poi lo passa
            return tokenHandler.WriteToken(token);

        }
    }
}