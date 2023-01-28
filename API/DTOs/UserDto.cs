// UserDto è una classe di Data Transfer Object (DTO) che viene utilizzata per 
// trasferire dati tra il livello dei servizi e il livello 
// dei controller in un'applicazione basata su ASP.NET Core. In questo caso specifico, 
// UserDto contiene i dati relativi all'utente, ovvero il nome utente (Username) e 
// il token (Token) generato durante l'autenticazione dell'utente. Questa classe viene 
// utilizzata per creare un oggetto che contiene solo le informazioni necessarie per 
// l'utente, in modo da nascondere eventuali dettagli di implementazione o di sicurezza 
// dal livello dei servizi.

namespace API.DTOs
{
    public class UserDto
    {
        // Proprietà che contiene il nome utente dell'utente
        public string Username { get; set; }
        
        // Proprietà che contiene il token di autenticazione dell'utente
        public string Token { get; set; }
    }
}
