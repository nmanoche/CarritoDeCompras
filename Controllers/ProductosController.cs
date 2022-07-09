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

namespace CarritoDeCompras.Controllers
{
    public class ProductosController : Controller
    {
        private readonly BaseDeDatos _context;

        public ProductosController(BaseDeDatos context)
        {
            _context = context;
        }

        [Authorize(Policy = "AdminRequerido")]
        // GET: TestProductos
        public async Task<IActionResult> Index()
        {
            var baseDeDatos = _context.Productos.Include(t => t.Categoria).Include(t => t.Marca);
            return View(await baseDeDatos.ToListAsync());
        }

        // GET: TestProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(t => t.Categoria)
                .Include(t => t.Marca)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: TestProductos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");
            return View();
        }

        // POST: TestProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // GET: TestProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // POST: TestProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion", producto.IdCategoria);
            //ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
            return View(producto);
        }

        // GET: TestProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Producto = await _context.Productos
                .Include(t => t.Categoria)
                .Include(t => t.Marca)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (Producto == null)
            {
                return NotFound();
            }

            return View(Producto);
        }

        // POST: TestProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
