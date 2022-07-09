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
using Microsoft.AspNetCore.Authorization;
using CarritoDeCompras.Areas.Identity.Data;
using CarritoDeCompras.Areas.Identity.Pages;

namespace CarritoDeCompras.Controllers
{
    [System.Web.Mvc.Authorize]
    public class UsuariosController : Controller
    {
        private readonly BaseDeDatos _context;
        private readonly IdentityBaseDeDatos _contextIdentity;

        public UsuariosController(BaseDeDatos context, IdentityBaseDeDatos contextIdentity)
        {
            _context = context;
            _contextIdentity = contextIdentity;
        }

        [Authorize(Policy = "AdminRequerido")]
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        [Authorize(Policy = "AdminRequerido")]
        // GET: Usuarios
        public async Task<IActionResult> IndexIdentity()
        {
            return View(await _contextIdentity.Users.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _contextIdentity.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        public bool ExisteCorreoEnBaseDeDatos (IdentityUsuario usuario)
        {
           return _contextIdentity.Users.Where(m => m.Email == usuario.Email).Any();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido,Correo,Contrasena")] IdentityUsuario usuario)
        {
            if (ModelState.IsValid)
            {
                
                if (!this.ExisteCorreoEnBaseDeDatos(usuario))
                {
                    _contextIdentity.Add(usuario);
                await _contextIdentity.SaveChangesAsync();
                return RedirectToAction(nameof(IndexIdentity));
                }
                else
                {
                    ViewBag.Error = "***El correo ingresado ya existe en la base de datos***";
                }
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _contextIdentity.Users.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityUsuario usuario)
        {
            
            if (ModelState.IsValid)
            {
               
                var usuarioEnDB = _contextIdentity.Users.FirstOrDefault(m => m.Id == usuario.Id);
                    
                try
                {  
                    if (usuarioEnDB.Email == usuario.Email)
                    {
                        //usuarioEnDB.Email = usuario.Email;
                        usuarioEnDB.Nombre = usuario.Nombre;
                        usuarioEnDB.Apellido = usuario.Apellido;
                        usuarioEnDB.UserName = usuario.UserName;
                        _contextIdentity.Update(usuarioEnDB);
                        await _contextIdentity.SaveChangesAsync();
                        return RedirectToAction(nameof(IndexIdentity));
                    }
                    else
                    {
                        if (!ExisteCorreoEnBaseDeDatos(usuario))
                        {
                            usuarioEnDB.Email = usuario.Email;
                            usuarioEnDB.Nombre = usuario.Nombre;
                            usuarioEnDB.Apellido = usuario.Apellido;
                            usuarioEnDB.UserName = usuario.UserName;
                            _contextIdentity.Update(usuarioEnDB);
                            await _contextIdentity.SaveChangesAsync();
                            return RedirectToAction(nameof(IndexIdentity));
                        }
                        else
                        {
                            ViewBag.Error = "***El correo ingresado ya existe en la base de datos***";
                        }
                    }
                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                             
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _contextIdentity.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _contextIdentity.Users.FindAsync(id);
            _contextIdentity.Users.Remove(usuario);
            await _contextIdentity.SaveChangesAsync();
            return RedirectToAction(nameof(IndexIdentity));
        }

        private bool UsuarioExists(string id)
        {
            return _contextIdentity.Users.Any(e => e.Id == id);
        }
    }
}
