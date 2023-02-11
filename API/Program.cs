using API.Data;
using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Aggiunge Swagger per generare documentazione API automaticamente
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Collegamento alla classe estesa ApplicationServicesExtensions.cs
// Questa riga di codice consente di aggiungere i servizi necessari per l'applicazione
builder.Services.AddApplicationServices(builder.Configuration);

//Collegamento alla classe estesa IdentityServiceExtensions.cs
// Questa riga di codice consente di aggiungere i servizi necessari per l'identità dell'utente
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>(); // Utilizzo del middleware personalizzato "ExceptionMiddleware" per la gestione degli errori nell'applicazione.

//Configurazione CORS
// consente richieste da qualsiasi origine, qualsiasi intestazione e qualsiasi metodo con un'origine specifica
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
// Aggiunge Swagger per generare documentazione API automaticamente
app.UseSwagger();
app.UseSwaggerUI();
}

// Reindirizzamento HTTPS
app.UseHttpsRedirection();

//Configurazione Accesso Token
// Verifica che sia presente un token valido per l'autenticazione
app.UseAuthentication();
// Accetta il token valido per l'autorizzazione
app.UseAuthorization();

// Mappa i controllers per le richieste
app.MapControllers();

//Serve a ricreare un db pulito prendendo i dati direttamente dal json, se il db non esiste ne crea uno nuovo, 
//se esiste invece mantiene quello esistente
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try 
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

// Esegue l'applicazione
app.Run();