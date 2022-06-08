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

namespace CarritoDeCompras.Controllers
{
    public class TestProductosController : Controller
    {
        private readonly BaseDeDatos _context;

        public TestProductosController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: TestProductos
        public async Task<IActionResult> Index()
        {
            var baseDeDatos = _context.TestProductos.Include(t => t.Categoria).Include(t => t.Marca);
            return View(await baseDeDatos.ToListAsync());
        }

        // GET: TestProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testProducto = await _context.TestProductos
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (testProducto == null)
            {
                return NotFound();
            }

            return View(testProducto);
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
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen")] TestProducto testProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "Descripcion", testProducto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", testProducto.IdMarca);
            return View(testProducto);
        }

        // GET: TestProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testProducto = await _context.TestProductos.FindAsync(id);
            if (testProducto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", testProducto.IdCategoria);
            return View(testProducto);
        }

        // POST: TestProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,IdMarca,IdCategoria,Precio,Stock,RutaImagen")] TestProducto testProducto)
        {
            if (id != testProducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestProductoExists(testProducto.IdProducto))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categorias, "IdCategoria", "IdCategoria", testProducto.IdCategoria);
            return View(testProducto);
        }

        // GET: TestProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testProducto = await _context.TestProductos
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (testProducto == null)
            {
                return NotFound();
            }

            return View(testProducto);
        }

        // POST: TestProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testProducto = await _context.TestProductos.FindAsync(id);
            _context.TestProductos.Remove(testProducto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestProductoExists(int id)
        {
            return _context.TestProductos.Any(e => e.IdProducto == id);
        }
    }
}
