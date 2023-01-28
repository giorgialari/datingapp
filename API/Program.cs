using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

// Esegue l'applicazione
app.Run();