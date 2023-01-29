// Questo codice crea una classe chiamata "ApiException" all'interno del namespace "API.Errors". La classe ha un costruttore pubblico che accetta tre parametri: un intero "statusCode", una stringa "message" e una stringa "details". 
// All'interno del costruttore, i valori dei parametri vengono 
// assegnati alle proprietà corrispondenti della classe 
// "StatusCode", "Message" e "Details".
// Inoltre, sono state create tre proprietà pubbliche, "
// StatusCode" di tipo intero, "Message" di tipo stringa, 
// "Details" di tipo stringa. Queste proprietà rappresentano 
// rispettivamente il codice di stato dell'eccezione, il messaggio 
// dell'eccezione e i dettagli dell'eccezione.

namespace API.Errors
{
    public class ApiException
    {
        // Crea una nuova istanza della classe ApiException con i valori del codice di stato, del messaggio e dei dettagli specificati
        public ApiException(int statusCode, string message, string details)
        {
            // Assegna il valore del codice di stato alla proprietà StatusCode
            StatusCode = statusCode;
            // Assegna il valore del messaggio alla proprietà Message
            Message = message;
            // Assegna il valore dei dettagli alla proprietà Details
            Details = details;
        }
        // Proprietà che rappresenta il codice di stato dell'eccezione
        public int StatusCode { get; set; }
        // Proprietà che rappresenta il messaggio dell'eccezione
        public string Message { get; set; }
        // Proprietà che rappresenta i dettagli dell'eccezione
        public string Details { get; set; }
    }
}