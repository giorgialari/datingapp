using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) //estende la classe DbContext di Entity Framework Core e serve come punto di ingresso per l'interazione con il database. 
        {
        }
        // Dichiarazione del DbSet per la classe AppUser
        public DbSet<AppUser> Users { get; set; }
    }

}