using CarritoDeCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeCompras.Datos
{
    public class BaseDeDatos : DbContext
    {
        public BaseDeDatos(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .HasOne<Categoria>(s => s.Categoria)
                .WithMany(g => g.Productos)
                .HasForeignKey(s => s.IdCategoria);

            modelBuilder.Entity<Producto>()
                .HasOne<Marca  >(s => s.Marca)
                .WithMany(g => g.Productos)
                .HasForeignKey(s => s.IdMarca);
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        //public DbSet<Producto> Productos { get; set; }
        public DbSet<CarritoDeCompras.Models.Categoria>? Categorias { get; set; }
        public DbSet<CarritoDeCompras.Models.Marca>? Marcas { get; set; }
        public DbSet<CarritoDeCompras.Models.Producto>? Productos { get; set; }
        public DbSet<CarritoDeCompras.Models.Carrito>? Carritos { get; set; }
        public DbSet<DetalleCompra>? DetalleCompras { get; set; }
    }
}
