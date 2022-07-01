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
            var listaProductosEnCarrito = await _context.Carritos.Where(u => u.IdUsuario == idUser).ToListAsync();

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
