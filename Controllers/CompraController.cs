using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarritoDeCompras.Controllers;
using CarritoDeCompras.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using CarritoDeCompras.Datos;
using CarritoDeCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeCompras.Controllers
{
    public class CompraController : Controller
    {

        private readonly BaseDeDatos _context;
        private readonly UserManager<IdentityUsuario> _userManager;

        public CompraController(BaseDeDatos context, UserManager<IdentityUsuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CompraController
        public async Task<IActionResult> Index()
        {
            return View(await ObtenerListaProductosdelUsuario());
        }

        public async Task<List<ProductoMostrable>> ObtenerListaProductosdelUsuario()
        {
            var idUser = _userManager.GetUserId(User);
            var listaProductosEnCarrito = await _context.Carritos.Where(u => u.IdUsuario == idUser && u.Activo != 0).ToListAsync();

            List<ProductoMostrable> listaDeProductosMostrar = new List<ProductoMostrable>();
            foreach (var item in listaProductosEnCarrito)
            {
                var idProducto = item.IdProducto;
                var producto = _context.Productos.Where(p => p.IdProducto == idProducto).FirstOrDefault();
                var cantidad = item.Cantidad;
                listaDeProductosMostrar.Add(new ProductoMostrable(producto, (int)cantidad));
            }

            return listaDeProductosMostrar;
        }

        public async Task<IActionResult> PagarCompra(string? NombreApellidoFacturar, string? EmailFacturar, string? DireccionFacturar, string? CodigoPostalFacturar)
        {
            var idUser = _userManager.GetUserId(User);
            var fechaCompra = DateTime.Today.ToString();
            var totalPagar = CalcularTotalPagar(idUser);
            var nombreApellido = NombreApellidoFacturar;
            var emailCompra = EmailFacturar;
            var direccionCompra = DireccionFacturar;
            var codigoPostal = CodigoPostalFacturar;

            Compra nuevaCompra = new Compra(idUser, fechaCompra, totalPagar, nombreApellido, emailCompra, direccionCompra, codigoPostal);
            _context.Add(nuevaCompra);
            _context.SaveChanges();

            InactivarCarritoDeUsuario();

            return View();
        }

        private decimal CalcularTotalPagar(string idUser)
        {
            decimal totalAPagar = 0;
            var listaProductosEnCarrito = _context.Carritos.Where(u => u.IdUsuario == idUser && u.Activo != 0).ToList();

            foreach (var item in listaProductosEnCarrito)
            {
                var cantidadProductos = item.Cantidad;
                var producto = _context.Productos.Where(p => p.IdProducto == item.IdProducto).FirstOrDefault();
                var precioProducto = producto.Precio;
                totalAPagar += (decimal)cantidadProductos * (decimal)precioProducto;
            }
            return totalAPagar;
        }

        private void InactivarCarritoDeUsuario()
        {
            var idUser = _userManager.GetUserId(User);
            var listaProductosEnCarritoActivo = _context.Carritos.Where(u => u.IdUsuario == idUser && u.Activo != 0).ToList();

            foreach (var item in listaProductosEnCarritoActivo)
            {
                item.Activo = 0;
                _context.Update(item);
                _context.SaveChanges();
            }
        }

        // GET: CompraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
