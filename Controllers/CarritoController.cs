using CarritoDeCompras.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarritoDeCompras.Models;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeCompras.Controllers
{
    public class CarritoController : Controller
    {

        private readonly BaseDeDatos _context;

        public CarritoController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: CarritoController
        //[Authorize(Policy = "AdminRequerido")]
        //[Authorize(Roles ="Administrador, Cliente")]
        public async Task<IActionResult> Index(string idUser)
        {
            if (TempData["AgregadoCarrito"] != null)
            {
                ViewBag.AgregadoCarrito = TempData["AgregadoCarrito"].ToString();
            }

            var listaProductosEnCarrito = await _context.Carritos.Where(u => u.IdUsuario == idUser).ToListAsync();

            List<ProductoMostrable> listaDeProductosMostrar = new List<ProductoMostrable>();
            foreach (var item in listaProductosEnCarrito)
            {
                var idProducto = item.IdProducto;
                var producto = _context.Productos.Where(p => p.IdProducto == idProducto).FirstOrDefault();
                var cantidad = item.Cantidad;
                listaDeProductosMostrar.Add(new ProductoMostrable(producto, (int)cantidad));
            }

            return View(listaDeProductosMostrar);
        }

        public bool ExisteCarrito(string idClienteUsuario, int idProducto)
        {
            bool existeCarrito = _context.Carritos.Where(car => car.IdUsuario == idClienteUsuario && car.IdProducto == idProducto).Any();

            return existeCarrito;
        }

        public int CantidadProductosEnCarrito(string idClienteUsuario)
        {
            int cantidadProductos = 0;

            Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();

            if(carrito != null)
            {
                cantidadProductos = _context.Carritos.Count(us => us.IdUsuario == idClienteUsuario);   
            }

            return cantidadProductos;
        }

        [HttpGet]
        public JsonResult CantidadProductosEnCarritoJson(string idClienteUsuario)
        {
            int cantidadProductos = 0;

            Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();

            if (carrito != null)
            {
                cantidadProductos = _context.Carritos.Count(us => us.IdUsuario == idClienteUsuario);
            }

            return Json(new { cantidad = cantidadProductos });
        }

        //public void OperarConCarrito(int idClienteUsuario, int idProducto, bool agregarProducto)
        //{
        //    if(agregarProducto)
        //    {
        //        if(ExisteCarrito(idClienteUsuario, idProducto))
        //        {
        //            Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();
        //            carrito.Cantidad += 1;
        //            _context.SaveChanges();
        //        }else
        //        {
        //            _context.Carritos.Add(new Carrito());
        //            _context.SaveChanges();
        //        }

        //    }
        //}

        // GET: CarritoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarritoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarritoController/Create
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

        // GET: CarritoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarritoController/Edit/5
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

        // GET: CarritoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarritoController/Delete/5
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

        public async Task<IActionResult> AgregarAlCarrito(int idProducto, string idUser)
        {
            //var user = await _contextIdentity.Users.FirstOrDefaultAsync(id => id.Id == idUser);

            var cart = await _context.Carritos.FirstOrDefaultAsync(id => id.IdUsuario == idUser && id.IdProducto == idProducto);

            var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == idProducto);
            string descripcionProducto = producto.Descripcion;

            if (cart == null)
            {
                Carrito newCart = new Carrito(idProducto, idUser, 1);
                _context.Add(newCart);
                await _context.SaveChangesAsync();
                DescontarStockDeProducto(producto);

                TempData["AgregadoCarrito"] = descripcionProducto + " fue agregado correctamente al carrito de compras.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SumarCantidadProductoCarrito(cart);
                DescontarStockDeProducto(producto);
                TempData["AgregadoCarrito"] = "Se agregó otra unidad de " + descripcionProducto + " al carrito de compras.";
                return RedirectToAction(nameof(Index));
            }


            //return RedirectToAction(nameof(Index));
        }

        private void SumarCantidadProductoCarrito([Bind("IdCarrito,IdProducto,IdUsuario,Cantidad")] Carrito carrito)
        {
            carrito.Cantidad += 1;
            _context.Update(carrito);
            _context.SaveChanges();
        }

        private void DescontarStockDeProducto(Producto producto)
        {
            producto.Stock -= 1;
            _context.Update(producto);
            _context.SaveChanges();
        }
    }
}
