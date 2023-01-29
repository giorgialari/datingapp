using System.Net; //importa la classe HttpStatusCode
using System.Text.Json; //importa la classe JsonSerializer
using API.Errors; //importa la classe ApiException

namespace API.Middleware //definisce la classe "ExceptionMiddleware" all'interno del namespace "API.Middleware"
{
    public class ExceptionMiddleware //definisce la classe "ExceptionMiddleware"
    {
        private readonly RequestDelegate _next; //dichiarazione di una variabile privata "RequestDelegate" chiamata "_next"
        private readonly ILogger _logger; //dichiarazione di una variabile privata "ILogger" chiamata "_logger"
        private readonly IHostEnvironment _env; //dichiarazione di una variabile privata "IHostEnvironment" chiamata "_env"



        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env) //costruttore della classe che prende 3 parametri: "next" di tipo "RequestDelegate", "logger" di tipo "ILogger<ExceptionMiddleware>" e "env" di tipo "IHostEnvironment"
        {
            _env = env; //assegna il valore del parametro "env" alla variabile privata "_env"
            _logger = logger; //assegna il valore del parametro "logger" alla variabile privata "_logger"
            _next = next; //assegna il valore del parametro "next" alla variabile privata "_next"
        }
        public async Task InvokeAsync(HttpContext context) //definisce il metodo "InvokeAsync" che prende un parametro "context" di tipo "HttpContext"
        {
            try //blocco try-catch per gestire eventuali eccezioni
            {
                await _next(context); //attende l'esecuzione del metodo "_next" passando come parametro "context"
            }

            catch (Exception ex) //gestisce le eccezioni di tipo "Exception"
            {
                _logger.LogError(ex, ex.Message); //utilizza il metodo "LogError" dell'oggetto "_logger" per registrare l'errore e il messaggio dell'eccezione

                context.Response.ContentType = "application/json"; //imposta il ContentType della risposta su "application/json"

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //imposta lo StatusCode della risposta su "InternalServerError"

                var response = _env.IsDevelopment() //verifica se l'ambiente di esecuzione è di sviluppo
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) //se è di sviluppo, crea un'istanza di ApiException con lo stato della risposta, il messaggio dell'eccezione originale e la traccia dello stack dell'eccezione originale
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error"); //se non è di sviluppo, crea un'istanza di ApiException con lo stato della risposta, il messaggio dell'eccezione originale e un messaggio generico "Internal Server Error" per la traccia dello stack
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // imposta le opzioni di serializzazione JSON per utilizzare la politica di denominazione delle proprietà in formato camelCase
                var json = JsonSerializer.Serialize(response, options); // serializza l'oggetto eccezione in formato JSON utilizzando le opzioni impostate
                await context.Response.WriteAsync(json); // scrive la risposta JSON nel contesto della richiesta HTTP
            }

        }

    }
}