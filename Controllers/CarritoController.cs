using CarritoDeCompras.Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarritoDeCompras.Models;

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
        public ActionResult Index()
        {
            return View();
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
    }
}
