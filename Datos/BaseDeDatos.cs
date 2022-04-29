using CarritoDeCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeCompras.Datos
{
    public class BaseDeDatos : DbContext
    {
        public BaseDeDatos(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
