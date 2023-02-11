using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; } // Id univoco per identificare l'utente
        public string UserName { get; set; } // Nome utente scelto dall'utente
        public byte[] PasswordHash { get; set; } // Hash della password dell'utente
        public byte[] PasswordSalt { get; set; } // Sale utilizzato durante la creazione dell'hash della password

        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new();

        public int GetAge()
        {
            return DateOfBirth.CalcuateAge();
        }


    }
}