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
        public IActionResult Index()
        {
            var datos = ListarProductosDisponiblesParaPublicar();
            return View(datos);
        }

        [HttpGet]
        public JsonResult ListarProductosDisponibles()
        {
            List<Producto> listaDeProductos = new List<Producto>();

            listaDeProductos = _context.Productos.Where(p => p.Stock > 0).ToList();

            var jsonResult = Json(new { data = listaDeProductos });

            return jsonResult;
        }

        [HttpGet]
        public List<Producto> ListarProductosDisponiblesParaPublicar()
        {
            List<Producto> listaDeProductos = new List<Producto>();

            listaDeProductos = _context.Productos.Where(p => p.Stock > 0).ToList();


            return listaDeProductos;
        }

    }
}
