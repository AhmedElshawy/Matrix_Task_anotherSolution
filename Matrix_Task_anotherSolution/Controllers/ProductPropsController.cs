using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matrix_Task_anotherSolution.Models;

namespace Matrix_Task_anotherSolution.Controllers
{
    public class ProductPropsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductPropsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductProps
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductProp.Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProductProps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductProp == null)
            {
                return NotFound();
            }

            var productProp = await _context.ProductProp
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productProp == null)
            {
                return NotFound();
            }

            return View(productProp);
        }

        // GET: ProductProps/Create
        public IActionResult Create(int id)
        {
            var product = _context.Product.Where(i=>i.Id == id).FirstOrDefault();
            var categoryProps = _context.CategoryProps.Where(c=>c.CategoryId == product.CategoryId).ToList();
            ViewData["categoryPropsId"] = new SelectList(categoryProps, "Id", "Name");
            ViewData["ProductId"] = id;
            return View();
        }

        // POST: ProductProps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,ProductId")] ProductProp productProp , int categoryPropsId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productProp);
                await _context.SaveChangesAsync();
                var ObjectToSend = new {categoryPropId= categoryPropsId,productPropId= productProp.Id };
                return RedirectToAction(nameof(Create), "ProductCategoryProps", ObjectToSend);
            }            
            return View(productProp);
        }

        // GET: ProductProps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductProp == null)
            {
                return NotFound();
            }

            var productProp = await _context.ProductProp.FindAsync(id);
            if (productProp == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productProp.ProductId);
            return View(productProp);
        }

        // POST: ProductProps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ProductId")] ProductProp productProp)
        {
            if (id != productProp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productProp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPropExists(productProp.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", productProp.ProductId);
            return View(productProp);
        }

        // GET: ProductProps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductProp == null)
            {
                return NotFound();
            }

            var productProp = await _context.ProductProp
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productProp == null)
            {
                return NotFound();
            }

            return View(productProp);
        }

        // POST: ProductProps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductProp == null)
            {
                return Problem("Entity set 'AppDbContext.ProductProp'  is null.");
            }
            var productProp = await _context.ProductProp.FindAsync(id);
            if (productProp != null)
            {
                _context.ProductProp.Remove(productProp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPropExists(int id)
        {
          return _context.ProductProp.Any(e => e.Id == id);
        }
    }
}
