using CarritoDeCompras.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarritoDeCompras.Areas.Identity.Data;

public class IdentityBaseDeDatos : IdentityDbContext<IdentityUsuario>
{
    public IdentityBaseDeDatos(DbContextOptions<IdentityBaseDeDatos> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<IdentityUsuario>
{
    public void Configure(EntityTypeBuilder<IdentityUsuario> builder)
    {
        builder.Property(u => u.Nombre).HasMaxLength(255);
        builder.Property(u => u.Apellido).HasMaxLength(255);
    }
}