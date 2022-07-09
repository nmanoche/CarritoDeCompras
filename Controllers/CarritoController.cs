using CarritoDeCompras.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarritoDeCompras.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CarritoDeCompras.Areas.Identity.Data;

namespace CarritoDeCompras.Controllers
{
    public class CarritoController : Controller
    {

        private readonly BaseDeDatos _context;
        private readonly UserManager<IdentityUsuario> _userManager;

        public CarritoController(BaseDeDatos context, UserManager<IdentityUsuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CarritoController
        //[Authorize(Policy = "AdminRequerido")]
        //[Authorize(Roles ="Administrador, Cliente")]
        public async Task<IActionResult> Index()
        {
            if (TempData["MsgCarritoVacio"] != null)
            {
                ViewBag.VaciarCarrito = TempData["MsgCarritoVacio"].ToString();
            }

            //var listaProductosEnCarrito = await _context.Carritos.Where(u => u.IdUsuario == idUser).ToListAsync();

            //List<ProductoMostrable> listaDeProductosMostrar = new List<ProductoMostrable>();
            //foreach (var item in listaProductosEnCarrito)
            //{
            //    var idProducto = item.IdProducto;
            //    var producto = _context.Productos.Where(p => p.IdProducto == idProducto).FirstOrDefault();
            //    var cantidad = item.Cantidad;
            //    listaDeProductosMostrar.Add(new ProductoMostrable(producto, (int)cantidad));
            //}

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
                var producto = _context.Productos.
                                Where(p => p.IdProducto == idProducto).
                                Include(p => p.Marca).
                                FirstOrDefault();
                var cantidad = item.Cantidad;
                listaDeProductosMostrar.Add(new ProductoMostrable(producto, (int)cantidad));
            }

            return listaDeProductosMostrar;
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
            
            var cart = await _context.Carritos.FirstOrDefaultAsync(id => id.IdUsuario == idUser && id.IdProducto == idProducto && id.Activo != 0);

            var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == idProducto);
            string descripcionProducto = producto.Nombre;

            if (cart == null)
            {
                Carrito newCart = new Carrito(idProducto, idUser, 1);
                _context.Add(newCart);
                await _context.SaveChangesAsync();
                DescontarStockDeProducto(producto);

                TempData["AgregadoCarrito"] = descripcionProducto + " fue agregado correctamente al carrito de compras.";
                return RedirectToAction("Index", "Tienda");
            }
            else
            {
                SumarCantidadProductoCarrito(cart);
                DescontarStockDeProducto(producto);
                TempData["AgregadoCarrito"] = "Se agregó otra unidad de " + descripcionProducto + " al carrito de compras.";
                return RedirectToAction("Index", "Tienda");
            }

         }

        public async Task<IActionResult> QuitarProductoDelCarrito(int idProducto, string idUser)
        {
            //Busco el carrito que corresponde al producto y usuario logeado
            var cart = await _context.Carritos.FirstOrDefaultAsync(id => id.IdUsuario == idUser && id.IdProducto == idProducto && id.Activo != 0);

            if(cart == null)
            {
                return NotFound();
            }else
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == idProducto);
                RetornarStockDeProducto(producto, (int)cart.Cantidad);
                _context.Remove(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> VaciarCarrito()
        {
            //Busco el usuario logeado
            var idUser = _userManager.GetUserId(User);

            //Busco el carrito que corresponde al usuario logeado y activo
            var listaProductosEnCarrito = await _context.Carritos.Where(id => id.IdUsuario == idUser && id.Activo != 0).ToListAsync();

            if (listaProductosEnCarrito == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var item in listaProductosEnCarrito)
                {
                    var productoQuitar = _context.Productos.FirstOrDefault(p => p.IdProducto == item.IdProducto);
                    var cantidadQuitar = (int) item.Cantidad;
                    RetornarStockDeProducto(productoQuitar, cantidadQuitar);
                    _context.Remove(item);
                    await _context.SaveChangesAsync();

                }

                TempData["MsgCarritoVacio"] = "Todos los productos han sido eliminados del carrito de compra";

                return RedirectToAction("Index");
            }

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

        private void RetornarStockDeProducto(Producto producto, int cantidadRetornar)
        {
            producto.Stock += cantidadRetornar;
            _context.Update(producto);
            _context.SaveChanges();
        }
    }
}
