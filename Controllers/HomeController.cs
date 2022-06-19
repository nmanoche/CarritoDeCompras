#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarritoDeCompras.Datos;
using CarritoDeCompras.Models;
using System.Web;

namespace CarritoDeCompras.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly BaseDeDatos _context;

        public HomeController(BaseDeDatos context)
        {
            _context = context;
        }

        public bool ExisteCorreoEnBaseDeDatos(Usuario usuario)
        {
            return _context.Usuarios.Where(m => m.Correo == usuario.Correo).Any();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string contrasena)
        {
            Usuario usuario = new Usuario();

            usuario = _context.Usuarios.FirstOrDefault(m => m.Correo == correo && m.Contrasena == contrasena);

            if(usuario == null)
            {
                ViewBag.Error = "Correo o Contraseña inválidos";
                return View();
            }else
            {
                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
           
                return View();

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(Usuario nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                if (nuevoUsuario.Nombre == null || nuevoUsuario.Apellido == null || nuevoUsuario.Contrasena == null || nuevoUsuario.Correo == null)
                {
                    ViewBag.Error = "Por favor complete todos los campos";
                    return View();
                }
                else
                {
                    if (!this.ExisteCorreoEnBaseDeDatos(nuevoUsuario))
                    {
                        _context.Add(nuevoUsuario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Error = "El correo ingresado ya existe en la base de datos";
                        return View();
                    }
                }
            }
            return View(nuevoUsuario);

        }

        public ActionResult CerrarSesion()
        {
            return RedirectToAction("Login", "Home");
        }

    }
}