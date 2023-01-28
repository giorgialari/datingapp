// La classe RegisterDto rappresenta i dati necessari per 
// registrare un nuovo utente, comprende due proprietà: Username e Password. 
// Entrambe le proprietà sono contrassegnate con l'attributo Required, che indica che 
// questi campi sono obbligatori. In altre parole questa classe rappresenta un Data Transfer Object (DTO) 
// per la registrazione di un nuovo utente.


using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] // Indica che il campo è obbligatorio
        public string Username { get; set; }

        [Required] // Indica che il campo è obbligatorio
        public string Password { get; set; }
    }
}
