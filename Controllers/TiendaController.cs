using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarritoDeCompras.Models;
using CarritoDeCompras.Datos;
using CarritoDeCompras.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CarritoDeCompras.Controllers
{
    public class TiendaController : Controller
    {
        private readonly BaseDeDatos _context;
        private readonly IdentityBaseDeDatos _contextIdentity;
        private readonly SignInManager<IdentityUsuario> _signInManager;
        private readonly UserManager<IdentityUsuario> _userManager;

        public TiendaController(BaseDeDatos context, SignInManager<IdentityUsuario> signInManager, UserManager<IdentityUsuario> userManager, IdentityBaseDeDatos contextIdentity)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _contextIdentity = contextIdentity;
        }

        //[Authorize(Policy = "AdminRequerido")]
        //[Authorize(Roles ="Administrador, Cliente")]
        public async Task<IActionResult> Index()
        {
            if (TempData["AgregadoCarrito"] != null)
            {
                ViewBag.AgregadoCarrito = TempData["AgregadoCarrito"].ToString();
            }

            return View(await _context.Productos.Where(p => p.Stock > 0).ToListAsync());
        }

        [HttpGet]
        public JsonResult ListarProductosDisponibles()
        {
            List<Producto> listaDeProductos = new List<Producto>();

            listaDeProductos = _context.Productos.Where(p => p.Stock > 0).ToList();

            var jsonResult = Json(new { data = listaDeProductos });

            return jsonResult;
        }

        // GET: TestProductos/Details/5
        public async Task<IActionResult> DetalleProducto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
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

            if (carrito != null)
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

        public void OperarConCarrito(string idClienteUsuario, int idProducto, bool agregarProducto)
        {
            if (agregarProducto)
            {
                if (ExisteCarrito(idClienteUsuario, idProducto))
                {
                    Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();
                    carrito.Cantidad += 1;
                    _context.SaveChanges();
                }
                //else
                //{
                //    _context.Carritos.Add(new Carrito());
                //    _context.SaveChanges();
                //}

            }
        }

        //[HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int idProducto, string idUser)
        {
            //var user = await _contextIdentity.Users.FirstOrDefaultAsync(id => id.Id == idUser);
            var user = _userManager.GetUserId(User);

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
            }else
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
