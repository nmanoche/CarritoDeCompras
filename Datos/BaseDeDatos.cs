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
            modelBuilder.Entity<TestProducto>()
                .HasOne<Categoria>(s => s.Categoria)
                .WithMany(g => g.TestProductos)
                .HasForeignKey(s => s.IdCategoria);

            modelBuilder.Entity<TestProducto>()
                .HasOne<Marca  >(s => s.Marca)
                .WithMany(g => g.TestProductos)
                .HasForeignKey(s => s.IdMarca);
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<CarritoDeCompras.Models.Categoria> Categorias { get; set; }
        public DbSet<CarritoDeCompras.Models.Marca> Marcas { get; set; }
        public DbSet<CarritoDeCompras.Models.TestProducto> TestProductos { get; set; }
    }
}
