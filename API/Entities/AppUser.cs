

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } // Id univoco per identificare l'utente
        public string UserName { get; set; } // Nome utente scelto dall'utente
        public byte[] PasswordHash { get; set; } // Hash della password dell'utente
        public byte[] PasswordSalt { get; set; } // Sale utilizzato durante la creazione dell'hash della password
    }
}