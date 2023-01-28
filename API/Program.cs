using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Collegamento alla classe estesa ApplicationServicesExtensions.cs
builder.Services.AddApplicationServices(builder.Configuration);

//Collegamento alla classe estesa IdentityServiceExtensions.cs
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

//Configurazione CORS
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Configurazione Accesso Token
app.UseAuthentication(); // verifica che abbia un token valido
app.UseAuthorization(); // accetta il token valido

app.MapControllers();

app.Run();
