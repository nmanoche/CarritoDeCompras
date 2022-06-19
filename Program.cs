using CarritoDeCompras.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarritoDeCompras.Areas.Identity.Data;
using BaseDeDatos = CarritoDeCompras.Datos.BaseDeDatos;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BaseDeDatosConnection") ?? throw new InvalidOperationException("Connection string 'BaseDeDatosConnection' not found.");

//builder.Services.AddDbContext<IdentityBaseDeDatos>(options =>
//    options.UseSqlServer(connectionString));;

builder.Services.AddDbContext<IdentityBaseDeDatos>(options =>
    options.UseSqlite(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUsuario>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityBaseDeDatos>();;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext <BaseDeDatos>(options => 
  options.UseSqlite(@"filename=C:\PNT1\CarritoDeCompras\DB_CarritoDeCompras.db"));
//C:\PNT1\CarritoDeCompras
#region Authorizacion

AddAuthorizationPolicies(builder.Services);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(" / Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies(IServiceCollection services)
{
    services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminRequerido", policy => policy.RequireRole("Administrador"));
    });
}
