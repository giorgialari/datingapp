using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization; // Include del namespace per l'autorizzazione
using Microsoft.AspNetCore.Mvc; // Include del namespace per il supporto di MVC
using Microsoft.EntityFrameworkCore; // Include del namespace per l'utilizzo di Entity Framework Core

namespace API.Controllers
{
    [Authorize] // Specifica che questo controller richiede autorizzazione per essere utilizzato
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context; // Inizializzazione di un'istanza del DataContext
        public UsersController(DataContext context)
        {
            _context = context; // Assegnazione del DataContext alla proprietà privata
        }

        [AllowAnonymous] // Specifica che questo metodo non richiede autorizzazione per essere utilizzato
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync(); // Caricamento dell'elenco degli utenti dal database

            return users; // Restituzione dell'elenco degli utenti
        }

        [HttpGet("{id}")] // Specifica che questo metodo può essere chiamato specificando un id come parametro nell'URL
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id); // Caricamento dell'utente dal database in base all'id specificato
        }
    }

}