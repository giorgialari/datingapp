using API.Data; //importiamo il namespace "Data" da "API"
using API.Entities; //importiamo il namespace "Entities" da "API"
using Microsoft.AspNetCore.Authorization; //importiamo il namespace "Authorization" da "Microsoft.AspNetCore"
using Microsoft.AspNetCore.Mvc; //importiamo il namespace "Mvc" da "Microsoft.AspNetCore"

namespace API.Controllers //creiamo un namespace chiamato "Controllers" all'interno di "API"
{
    public class BuggyController : BaseApiController //creiamo una classe chiamata "BuggyController" che eredita dalla classe "BaseApiController"
    {
        private readonly DataContext _context; //dichiariamo una variabile privata di tipo "DataContext" chiamata "_context"
        public BuggyController(DataContext context) //creiamo un costruttore che accetta un parametro di tipo "DataContext" chiamato "context"
        {
            _context = context; //assegnamo il valore del parametro alla variabile privata
        }

        [Authorize] //richiede l'autenticazione per questo metodo
        [HttpGet("auth")] //questo metodo risponde solo alle richieste GET con percorso "auth"
        public ActionResult<string> GetSecret() //dichiariamo un metodo chiamato "GetSecret" che restituisce un oggetto "ActionResult" di tipo string
        {
            return "secret text"; //restituiamo il testo "secret text"
        }

        [HttpGet("not-found")] //questo metodo risponde solo alle richieste GET con percorso "not-found"
        public ActionResult<AppUser> GetNotFound() //dichiariamo un metodo chiamato "GetNotFound" che restituisce un oggetto "ActionResult" di tipo "AppUser"
        {

            var thing = _context.Users.Find(-1); //cerchiamo un utente con id -1
            if (thing == null) return NotFound(); //se non esiste restituiamo un errore "NotFound"
            return thing; //altrimenti restituiamo l'utente trovato
        }

        [HttpGet("server-error")] //questo metodo risponde solo alle richieste GET con percorso "server-error"
        public ActionResult<string> GetServerError() //dichiariamo un metodo chiamato "GetServerError" che restituisce un oggetto "ActionResult" di tipo string
        {
                var thing = _context.Users.Find(-1); //cerchiamo un utente con id -1
                var thingToReturn = thing.ToString(); //convertiamo l'oggetto "thing" in una stringa
                return thingToReturn; //restituiamo la stringa
      
        }

        [HttpGet("bad-request")] //questo metodo risponde solo alle richieste GET con percorso "bad-request"
        public ActionResult GetBadRequest() //dichiariamo un metodo chiamato "GetBadRequest" che restituisce un oggetto "ActionResult" di tipo string
        {
            return BadRequest("This is a not good request"); //restituiamo un errore "BadRequest" con il messaggio "This is a not good request"
        }
    }
}