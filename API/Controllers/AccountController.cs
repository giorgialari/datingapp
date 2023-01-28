using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context; // Inizializzazione di un'istanza del DataContext
        private readonly ITokenService _tokenService; // Inizializzazione di un'istanza del servizio di token
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService; // Assegnazione del servizio di token alla proprietà privata
            _context = context; // Assegnazione del DataContext alla proprietà privata
        }

        [HttpPost("register")] // Metodo per la registrazione di un utente, richiede una richiesta POST all'endpoint api/account/register con i parametri username e password
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken"); // Se l'username esiste già, viene restituito un errore

            using var hmac = new HMACSHA512(); // Utilizzo dell'algoritmo HMACSHA512 per la generazione dell'hash della password

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(), // Il nome utente viene convertito in lowercase
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), // La password viene convertita in bytes e viene generato l'hash
                PasswordSalt = hmac.Key // La chiave utilizzata per generare l'hash viene utilizzata come salt
            };

            _context.Users.Add(user); // Il nuovo utente viene aggiunto al DataContext
            await _context.SaveChangesAsync(); // Viene salvato il nuovo utente nel database

            return new UserDto
            {
                Username = user.UserName, // Viene restituito il nome utente
                Token = _tokenService.CreateToken(user) // Viene generato e restituito il token per l'utente
            };
        }

        [HttpPost("login")] //POST: api/account/login?username=dave&password=pwd
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) // Metodo per effettuare il login
        {
            var user = await _context.Users.SingleOrDefaultAsync(x =>
            x.UserName == loginDto.Username); // Ricerca dell'utente con lo stesso nome utente passato nella richiesta
            if (user == null) return Unauthorized("invalid username"); // Se l'utente non esiste, ritorna un messaggio di errore

            using var hmac = new HMACSHA512(user.PasswordSalt); // Utilizzo dell'algoritmo HMACSHA512 per controllare la corrispondenza della password

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); // Creazione del hash della password inserita

            for (int i = 0; i < computedHash.Length; i++) // Confronto tra il hash inserito e quello salvato nel db
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password"); // Se non combaciano ritorna un messaggio di errore
            }

            return new UserDto // Se tutto è andato a buon fine, ritorna un nuovo oggetto UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username) // Metodo privato per verificare l'esistenza dell'utente
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower()); // Ritorna true se esiste un utente con lo stesso nome utente
        }
    }

}