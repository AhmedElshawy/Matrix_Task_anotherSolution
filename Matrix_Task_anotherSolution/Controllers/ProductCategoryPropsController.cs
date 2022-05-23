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
    public class ProductCategoryPropsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductCategoryPropsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProductCategoryProps
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.ProductCategoryProps.Include(p => p.ProductProp).Include(d=>d.CategoryProps)
                .ToListAsync();
            return View(appDbContext);
        }

        // GET: ProductCategoryProps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductCategoryProps == null)
            {
                return NotFound();
            }

            var productCategoryProp = await _context.ProductCategoryProps
                .Include(p => p.ProductProp)
                .FirstOrDefaultAsync(m => m.ProductPropId == id);
            if (productCategoryProp == null)
            {
                return NotFound();
            }

            return View(productCategoryProp);
        }

        // GET: ProductCategoryProps/Create
        public IActionResult Create(int categoryPropId, int productPropId)
        {            
            ViewData["ProductPropId"] = productPropId;
            ViewData["categoryPropId"] = categoryPropId;
            return View();
        }

        // POST: ProductCategoryProps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductPropId,CategoryPropId")] ProductCategoryProp productCategoryProp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategoryProp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductPropId"] = new SelectList(_context.ProductProp, "Id", "Id", productCategoryProp.ProductPropId);
            return View(productCategoryProp);
        }

        // GET: ProductCategoryProps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductCategoryProps == null)
            {
                return NotFound();
            }

            var productCategoryProp = await _context.ProductCategoryProps.FindAsync(id);
            if (productCategoryProp == null)
            {
                return NotFound();
            }
            ViewData["ProductPropId"] = new SelectList(_context.ProductProp, "Id", "Id", productCategoryProp.ProductPropId);
            return View(productCategoryProp);
        }

        // POST: ProductCategoryProps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductPropId,CategoryPropId")] ProductCategoryProp productCategoryProp)
        {
            if (id != productCategoryProp.ProductPropId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategoryProp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryPropExists(productCategoryProp.ProductPropId))
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
            ViewData["ProductPropId"] = new SelectList(_context.ProductProp, "Id", "Id", productCategoryProp.ProductPropId);
            return View(productCategoryProp);
        }

        // GET: ProductCategoryProps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductCategoryProps == null)
            {
                return NotFound();
            }

            var productCategoryProp = await _context.ProductCategoryProps
                .Include(p => p.ProductProp)
                .FirstOrDefaultAsync(m => m.ProductPropId == id);
            if (productCategoryProp == null)
            {
                return NotFound();
            }

            return View(productCategoryProp);
        }

        // POST: ProductCategoryProps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductCategoryProps == null)
            {
                return Problem("Entity set 'AppDbContext.ProductCategoryProps'  is null.");
            }
            var productCategoryProp = await _context.ProductCategoryProps.FindAsync(id);
            if (productCategoryProp != null)
            {
                _context.ProductCategoryProps.Remove(productCategoryProp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryPropExists(int id)
        {
          return _context.ProductCategoryProps.Any(e => e.ProductPropId == id);
        }
    }
}
