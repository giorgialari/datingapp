using Microsoft.AspNetCore.Mvc; // Importazione del namespace per utilizzare le classi del framework MVC di ASP.NET Core

namespace API.Controllers
{
    [ApiController] // Specifica che questa classe è un controller per le chiamate API
    [Route("api/[controller]")] // Specifica il prefisso delle route per questo controller
    public class BaseApiController : ControllerBase
    {
        // La classe non contiene alcun codice, è solo una classe base per altri controller
    }
}