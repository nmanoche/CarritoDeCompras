using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarritoDeCompras.Models;
using CarritoDeCompras.Datos;
using Microsoft.EntityFrameworkCore;

namespace CarritoDeCompras.Controllers
{
    public class TiendaController : Controller
    {
        private readonly BaseDeDatos _context;

        public TiendaController(BaseDeDatos context)
        {
            _context = context;
        }

        //[Authorize(Policy = "AdminRequerido")]
        //[Authorize(Roles ="Administrador, Cliente")]
        public async Task<IActionResult> Index()
        {
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

        public bool ExisteCarrito(int idClienteUsuario, int idProducto)
        {
            bool existeCarrito = _context.Carritos.Where(car => car.IdUsuario == idClienteUsuario && car.IdProducto == idProducto).Any();

            return existeCarrito;
        }

        public int CantidadProductosEnCarrito(int idClienteUsuario)
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
        public JsonResult CantidadProductosEnCarritoJson(int idClienteUsuario)
        {
            int cantidadProductos = 0;

            Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();

            if (carrito != null)
            {
                cantidadProductos = _context.Carritos.Count(us => us.IdUsuario == idClienteUsuario);
            }

            return Json(new { cantidad = cantidadProductos });
        }

        public void OperarConCarrito(int idClienteUsuario, int idProducto, bool agregarProducto)
        {
            if (agregarProducto)
            {
                if (ExisteCarrito(idClienteUsuario, idProducto))
                {
                    Carrito? carrito = _context.Carritos.Where(us => us.IdUsuario == idClienteUsuario).FirstOrDefault();
                    carrito.Cantidad += 1;
                    _context.SaveChanges();
                }
                else
                {
                    _context.Carritos.Add(new Carrito(idClienteUsuario, idProducto, 1));
                    _context.SaveChanges();
                }

            }
        }

    }
}
