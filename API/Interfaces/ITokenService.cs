//Interfaccia per la classe TokenService
using System;
using System.Collections.Generic;
using API.Entities;

namespace API.Interfaces
{
    //Interfaccia collegata alla classe TokenService.cs,
    //il tutto poteva funziona anche senza l'interfaccia, ma questa rende più facile i test unitari per creare un mock del test.
    public interface ITokenService
    {
        //Metodo che crea un token
        string CreateToken(AppUser user);
    }
}