// LoginDto è una classe DTO (Data Transfer Object) che serve a 
// rappresentare i dati che vengono inviati dal client 
// per effettuare l'operazione di login. In questo caso, 
// la classe contiene due proprietà: Username e Password 
// per memorizzare rispettivamente il nome utente e la 
// password inseriti dall'utente. Questi dati vengono utilizzati dall'applicazione 
// per verificare l'autenticità dell'utente e 
// consentirgli l'accesso alle funzionalità protette.

namespace API.DTOs
{
    public class LoginDto
    {
        // Proprietà che rappresenta il nome utente per l'autenticazione
        public string Username { get; set; }

        // Proprietà che rappresenta la password per l'autenticazione
        public string Password { get; set; }
    }
}
